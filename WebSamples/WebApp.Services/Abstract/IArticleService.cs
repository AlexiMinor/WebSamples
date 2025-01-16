using WebApp.Data.Entities;

namespace WebApp.Services.Abstract;

public interface IArticleService
{
    public Task<Article?[]> GetAllPositiveAsync(double minRate, int pageSize, int pageNumber);
    public Task<Article?> GetByIdAsync(Guid id);
    public Task AddArticleAsync(Article article);
    public Task<int> CountAsync(double minRate);

}