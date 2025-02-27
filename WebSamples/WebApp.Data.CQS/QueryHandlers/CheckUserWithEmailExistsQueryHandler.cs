using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.CQS.Queries;
using WebApp.Data.Entities;

namespace WebApp.Data.CQS.QueryHandlers;

public class CheckUserWithEmailExistsQueryHandler : IRequestHandler<CheckUserWithEmailExistsQuery, bool> //IQuery<Guid[]>
{
    private readonly ArticleAggregatorContext _dbContext;

    public CheckUserWithEmailExistsQueryHandler(ArticleAggregatorContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(CheckUserWithEmailExistsQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .AnyAsync(us=> us.Email.Equals(request.Email), cancellationToken);
    }
}
