using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApp.Data;
using WebApp.Data.Entities;
using WebApp.Services.Abstract;

namespace WebApp.Services.Implementations;

public class SourceService : ISourceService
{
    private readonly ArticleAggregatorContext _dbContext;
    private readonly ILogger<ArticleService> _logger;

    public SourceService(ArticleAggregatorContext dbContext, ILogger<ArticleService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }


    public async Task<Source[]> GetSourceWithRssAsync()
    {
        return await _dbContext.Sources
            .AsNoTracking()
            .Where(source => !string.IsNullOrWhiteSpace(source.RssUrl))
            .ToArrayAsync();
    }
}