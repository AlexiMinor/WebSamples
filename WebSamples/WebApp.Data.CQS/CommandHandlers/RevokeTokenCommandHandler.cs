using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.CQS.Commands;

namespace WebApp.Data.CQS.CommandHandlers;

public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand>
{
    private readonly ArticleAggregatorContext _context;

    public RevokeTokenCommandHandler(ArticleAggregatorContext context)
    {
        _context = context;
    }

    public async Task Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
    {
        var token = await _context.RefreshTokens
            .FirstOrDefaultAsync(x => x.Id == request.TokenId, cancellationToken);
        if (token != null)
        {
            token.IsRevoked = true;
            await _context.SaveChangesAsync(cancellationToken);
        }
        else
        {
            throw new Exception("Token not found");
        }
    }
}