using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Data.Entities;
using WebApp.Services.Abstract;

namespace WebApp.Services.Implementations;

public class ArticleService : IArticleService
{
    private readonly ArticleAggregatorContext _dbContext;
    
    public ArticleService(ArticleAggregatorContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Article?[]> GetAllPositiveAsync(double minRate, int pageSize, int pageNumber)
    {
        return await _dbContext.Articles
            .Where(article => article.PositivityRate >= minRate)
            .Include(article => article.Source)
            .AsNoTracking()
            .OrderByDescending(article => article.PositivityRate)
            .Skip((pageNumber-1)*pageSize)
            .Take(pageSize)
            .ToArrayAsync();
        }

    public async Task<Article?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Articles
            .AsNoTracking()
            .Include(article => article.Source)
            .FirstOrDefaultAsync(article => article.Id.Equals(id));
    }

    public async Task<int> CountAsync(double minRate)
    {
        return await _dbContext.Articles
            .AsNoTracking()
            .CountAsync();
    }

    public async Task AddArticleAsync(Article article)
    {
        await _dbContext.Articles.AddAsync(article);
        await _dbContext.SaveChangesAsync();
    }
}