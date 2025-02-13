using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.CQS.Queries;

namespace WebApp.Data.CQS.QueryHandlers;

public class GetIdsForArticlesWithNoTextQueryHandler : IRequestHandler<GetIdsForArticlesWithNoTextQuery, Guid[]> //IQuery<Guid[]>
{
    private readonly ArticleAggregatorContext _dbContext;

    public GetIdsForArticlesWithNoTextQueryHandler(ArticleAggregatorContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid[]> Handle(GetIdsForArticlesWithNoTextQuery request, CancellationToken cancellationToken)
    {
       return await _dbContext.Articles
            .AsNoTracking()
            .Where(article => !string.IsNullOrWhiteSpace(article.Url)
                              && string.IsNullOrWhiteSpace(article.Content))
            .Select(article => article.Id)
            .ToArrayAsync(cancellationToken);
    }
}
