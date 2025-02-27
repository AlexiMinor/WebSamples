using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.CQS.Commands;
using WebApp.Data.Entities;

namespace WebApp.Data.CQS.CommandHandlers;

public class TryToCreateRolesIfNecessaryCommandHandler : IRequestHandler<TryToCreateRolesIfNecessaryCommand>
{
    private readonly ArticleAggregatorContext _context;

    public TryToCreateRolesIfNecessaryCommandHandler(ArticleAggregatorContext context)
    {
        _context = context;
    }

    public async Task Handle(TryToCreateRolesIfNecessaryCommand request, CancellationToken cancellationToken)
    {
        string[] necessaryRoles = [ "User", "Admin" ];
        
        var existedRoles = await _context.Roles
            .AsNoTracking()
            .Where(r => necessaryRoles.Contains(r.Name))
            .Select(r => r.Name)
            .ToArrayAsync(cancellationToken);

        necessaryRoles = necessaryRoles.Except(existedRoles).ToArray();

        foreach (var role in necessaryRoles)
        {
            if (!_context.Roles.Any(r => r.Name == role))
            {
                _context.Roles.Add(new Role
                {
                    Id = Guid.NewGuid(),
                    Name = role
                });
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}