using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.CQS.Commands;
using WebApp.Data.Entities;

namespace WebApp.Data.CQS.CommandHandlers;

public class TryToRegisterCommandHandler : IRequestHandler<TryToRegisterUserCommand>
{
    private readonly ArticleAggregatorContext _context;

    public TryToRegisterCommandHandler(ArticleAggregatorContext context)
    {
        _context = context;
    }

    public async Task Handle(TryToRegisterUserCommand request, CancellationToken cancellationToken)
    {
        var role = await _context.Roles
            .AsNoTracking()
            .SingleOrDefaultAsync(role => role.Name.Equals("User"), 
                cancellationToken: cancellationToken);
        if (role != null)
        {
            await _context.Users.AddAsync(new User()
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                PasswordHash = request.PasswordHash,
                Name = request.Email,
                RoleId = role.Id

            }, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        else
        {
            throw new InvalidOperationException("Role not found");
        }
    }
}