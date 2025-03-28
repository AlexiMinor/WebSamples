using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.CQS.Queries;
using WebApp.Data.Entities;

namespace WebApp.Data.CQS.QueryHandlers;

public class GetArticlesWithoutRateQueryHandler : IRequestHandler<GetArticlesWithoutRateQuery, Article[]> //IQuery<Guid[]>
{
    private readonly ArticleAggregatorContext _dbContext;

    public GetArticlesWithoutRateQueryHandler(ArticleAggregatorContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Article[]> Handle(GetArticlesWithoutRateQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Articles
            .AsNoTracking()
            .Include(article => article.Source)
            .Where(article => !article.PositivityRate.HasValue)
            .ToArrayAsync(cancellationToken);
    }
}
