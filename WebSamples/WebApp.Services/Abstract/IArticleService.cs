using WebApp.Core.DTOs;
using WebApp.Data.Entities;

namespace WebApp.Services.Abstract;

public interface IArticleService
{
    public Task<ArticleDto?[]> GetAllPositiveAsync(double? minRate, int pageSize, int pageNumber, CancellationToken cancellationToken = default);
    public Task<ArticleDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    public Task AddArticleAsync(ArticleDto articleDto, CancellationToken cancellationToken = default);
    public Task<int> CountAsync(double minRate, CancellationToken cancellationToken = default);

    public Task<string[]> GetUniqueArticlesUrls(CancellationToken cancellationToken = default);

    public Task AddArticlesAsync(IEnumerable<Article> newUniqueArticles, CancellationToken cancellationToken = default);

    public Task UpdateTextForArticlesByWebScrappingAsync(CancellationToken cancellationToken);
}