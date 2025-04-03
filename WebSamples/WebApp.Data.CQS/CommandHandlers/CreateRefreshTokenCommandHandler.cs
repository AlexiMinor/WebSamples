using MediatR;
using WebApp.Data.CQS.Commands;
using WebApp.Data.Entities;

namespace WebApp.Data.CQS.CommandHandlers;

public class CreateRefreshTokenCommandHandler : IRequestHandler<CreateRefreshTokenCommand>
{
    private readonly ArticleAggregatorContext _context;

    public CreateRefreshTokenCommandHandler(ArticleAggregatorContext context)
    {
        _context = context;
    }

    public async Task Handle(CreateRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var rt = new RefreshToken()
        {
            Id = request.RefreshToken,
            UserId = request.UserId,
            Expires = request.ExpireAt
        };

        await _context.RefreshTokens.AddAsync(rt, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}