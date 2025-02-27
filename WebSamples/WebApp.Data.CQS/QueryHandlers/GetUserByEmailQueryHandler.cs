using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.CQS.Queries;
using WebApp.Data.Entities;

namespace WebApp.Data.CQS.QueryHandlers;

public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, User?> //IQuery<Guid[]>
{
    private readonly ArticleAggregatorContext _dbContext;

    public GetUserByEmailQueryHandler(ArticleAggregatorContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(user => user.Email.Equals(request.Email), cancellationToken);
    }
}
