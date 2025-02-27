using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.CQS.Queries;
using WebApp.Data.Entities;

namespace WebApp.Data.CQS.QueryHandlers;

public class GetPositiveArticlesWithPaginationQueryHandler : IRequestHandler<GetPositiveArticlesWithPaginationQuery, Article[]> //IQuery<Guid[]>
{
    private readonly ArticleAggregatorContext _dbContext;

    public GetPositiveArticlesWithPaginationQueryHandler(ArticleAggregatorContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Article[]> Handle(GetPositiveArticlesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var result = await _dbContext.Articles
            .AsNoTracking()
            //todo fix this when rating will be there
            .Where(article => article.PositivityRate >= request.PositivityRate || !article.PositivityRate.HasValue)
            .Include(article => article.Source)
            .OrderByDescending(article => article.CreationDate)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToArrayAsync(cancellationToken);

        return result;
    }
}
