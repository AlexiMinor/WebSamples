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


    public async Task<Source?[]> GetSourceWithRssAsync(CancellationToken cancellationToken=default)
    {
        return await _dbContext.Sources
            .AsNoTracking()
            .Where(source => !string.IsNullOrWhiteSpace(source.RssUrl))
            .ToArrayAsync(cancellationToken: cancellationToken);
    }

    public async Task<Source?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Sources
            .AsNoTracking()
            .FirstOrDefaultAsync(source => source.Id == id, cancellationToken);
    }
}