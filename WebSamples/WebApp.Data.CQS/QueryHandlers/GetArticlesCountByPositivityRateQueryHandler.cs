using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.CQS.Queries;
using WebApp.Data.Entities;

namespace WebApp.Data.CQS.QueryHandlers;

public class GetArticlesCountByPositivityRateQueryHandler : IRequestHandler<GetArticlesCountByPositivityRateQuery, int> //IQuery<Guid[]>
{
    private readonly ArticleAggregatorContext _dbContext;

    public GetArticlesCountByPositivityRateQueryHandler(ArticleAggregatorContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Handle(GetArticlesCountByPositivityRateQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Articles
            .AsNoTracking()
            .CountAsync(cancellationToken);
    }
}
