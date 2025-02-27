using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.CQS.Queries;
using WebApp.Data.Entities;

namespace WebApp.Data.CQS.QueryHandlers;

public class GetArticleByIdQueryHandler : IRequestHandler<GetArticleByIdQuery, Article?> //IQuery<Guid[]>
{
    private readonly ArticleAggregatorContext _dbContext;

    public GetArticleByIdQueryHandler(ArticleAggregatorContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Article?> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Articles
            .AsNoTracking()
            .Include(article => article.Source)
            .FirstOrDefaultAsync(article => article.Id.Equals(request.Id), cancellationToken);
    }
}
