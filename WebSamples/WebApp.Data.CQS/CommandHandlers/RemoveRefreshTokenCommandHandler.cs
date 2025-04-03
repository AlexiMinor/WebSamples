using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.CQS.Commands;

namespace WebApp.Data.CQS.CommandHandlers;

public class RemoveRefreshTokenCommandHandler : IRequestHandler<RemoveRefreshTokenCommand>
{
    private readonly ArticleAggregatorContext _context;

    public RemoveRefreshTokenCommandHandler(ArticleAggregatorContext context)
    {
        _context = context;
    }

    public async Task Handle(RemoveRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var rt = await _context.RefreshTokens
            .FirstOrDefaultAsync(x => x.Id == request.RefreshToken, cancellationToken);

        if (rt != null)
        {
            _context.RefreshTokens.Remove(rt);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}