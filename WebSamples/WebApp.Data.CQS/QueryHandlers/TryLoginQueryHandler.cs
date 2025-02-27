using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.CQS.Queries;
using WebApp.Data.Entities;

namespace WebApp.Data.CQS.QueryHandlers;

public class TryLoginQueryHandler : IRequestHandler<TryLoginQuery, User?> //IQuery<Guid[]>
{
    private readonly ArticleAggregatorContext _dbContext;

    public TryLoginQueryHandler(ArticleAggregatorContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> Handle(TryLoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Include(user => user.Role)
            .AsNoTracking()
            .SingleOrDefaultAsync(user => user.Email.Equals(request.Email),
            cancellationToken);

        return user != null && request.PasswordHash.Equals(user.PasswordHash) 
            ? user 
            : null;
    }
}
