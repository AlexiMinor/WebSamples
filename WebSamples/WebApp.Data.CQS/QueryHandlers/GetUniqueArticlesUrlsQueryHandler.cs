using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.CQS.Queries;
using WebApp.Data.Entities;

namespace WebApp.Data.CQS.QueryHandlers;

public class GetUniqueArticlesUrlsQueryHandler : IRequestHandler<GetUniqueArticlesUrlsQuery, string[]> //IQuery<Guid[]>
{
    private readonly ArticleAggregatorContext _dbContext;

    public GetUniqueArticlesUrlsQueryHandler(ArticleAggregatorContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string[]> Handle(GetUniqueArticlesUrlsQuery request, CancellationToken cancellationToken)
    {
        var result = await _dbContext.Articles
            .Select(article => article.Url)
            .AsNoTracking()
            .ToArrayAsync(cancellationToken);

        return result;
    }
}
