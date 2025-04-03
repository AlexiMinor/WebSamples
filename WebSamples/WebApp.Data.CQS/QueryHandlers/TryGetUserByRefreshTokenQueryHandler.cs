using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.CQS.Queries;
using WebApp.Data.Entities;

namespace WebApp.Data.CQS.QueryHandlers;

public class TryGetUserByRefreshTokenQueryHandler : IRequestHandler<TryGetUserByRefreshTokenQuery, User?> //IQuery<Guid[]>
{
    private readonly ArticleAggregatorContext _dbContext;

    public TryGetUserByRefreshTokenQueryHandler(ArticleAggregatorContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> Handle(TryGetUserByRefreshTokenQuery request, CancellationToken cancellationToken)
    {
        var rt = await _dbContext.RefreshTokens
            .AsNoTracking()
            .Include(rt=> rt.User)
            .ThenInclude(us=>us.Role)
            .FirstOrDefaultAsync(rt=> rt.Id.Equals(request.RefreshTokenId), cancellationToken);
        
        return rt is { IsActive: true } 
            ? rt.User 
            : null;
    }
}
