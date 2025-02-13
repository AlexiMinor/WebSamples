using HtmlAgilityPack;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApp.Core.DTOs;
using WebApp.Data;
using WebApp.Data.CQS.Commands;
using WebApp.Data.CQS.Queries;
using WebApp.Data.Entities;
using WebApp.MVC.Mappers;
using WebApp.Services.Abstract;

namespace WebApp.Services.Implementations;

public class ArticleService : IArticleService
{
    private readonly ArticleAggregatorContext _dbContext;
    private readonly IMediator _mediator;
    private readonly ILogger<ArticleService> _logger;
    private readonly ArticleMapper _mapper;

    public ArticleService(ArticleAggregatorContext dbContext, ILogger<ArticleService> logger, IMediator mediator, ArticleMapper mapper)
    {
        _dbContext = dbContext;
        _logger = logger;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<ArticleDto?[]> GetAllPositiveAsync(double? minRate, int pageSize, int pageNumber, CancellationToken cancellationToken = default)
    {
        try
        {
            return await (await _mediator.Send(new GetPositiveArticlesWithPaginationQuery()
            {
                PositivityRate = minRate,
                Page = pageNumber,
                PageSize = pageSize
            }, cancellationToken)).Select(article => _mapper.ArticleToArticleDto(article))
            .ToArrayAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while fetching articles");
            throw;
        }
    }

    public async Task<Article?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Articles
            .AsNoTracking()
            .Include(article => article.Source)
            .FirstOrDefaultAsync(article => article.Id.Equals(id), cancellationToken);
    }

    public async Task<int> CountAsync(double minRate, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Articles
            .AsNoTracking()
            .CountAsync(cancellationToken);
    }

    public async Task<string[]> GetUniqueArticlesUrls(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Articles
            .AsNoTracking()
            .Select(article => article.Url)
            .Distinct()
            .ToArrayAsync(cancellationToken);
    }

    public async Task AddArticlesAsync(IEnumerable<Article> newUniqueArticles, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(new AddArticlesCommand() { Articles = newUniqueArticles }, cancellationToken);
    }

    public async Task AddArticleAsync(ArticleDto articleDto, CancellationToken cancellationToken = default)
    {
        var article = _mapper.ArticleDtoToArticle(articleDto);
        article.Id = Guid.NewGuid();

        //todo add mediator & command
        await _dbContext.Articles.AddAsync(article, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateContentByWebScrappingAsync(Guid[] ids, CancellationToken token = default)
    {


    }

    public async Task UpdateTextForArticlesByWebScrappingAsync(CancellationToken cancellationToken = default)
    {
        var ids = await _mediator.Send(new GetIdsForArticlesWithNoTextQuery(), cancellationToken);

        //TODO: Move to web scrapping service with source checking and algorithm for getting article content
        var dictionary = new Dictionary<Guid, string>();
        foreach (var id in ids)
        {
            var article = await _dbContext.Articles
                .AsNoTracking()
                .FirstOrDefaultAsync(ar => ar.Id.Equals(id), cancellationToken);
            if (article == null || string.IsNullOrWhiteSpace(article.Url))
            {
                _logger.LogWarning("Article with id {id} not found or has no url", id);
                continue;
            }
            var web = new HtmlWeb();
            var doc = await web.LoadFromWebAsync(article.Url, cancellationToken);
            if (doc == null)
            {
                _logger.LogWarning("Failed to load article content from {url}", article.Url);
                continue;
            }
            var articleNode = doc.DocumentNode.SelectSingleNode("//div[@class='news-text']");
            if (articleNode == null)
            {
                _logger.LogWarning($"Failed to find correct article content at {article.Url}");
                continue;
            }
            dictionary.Add(id, articleNode.InnerHtml.Trim());
        }

        await _mediator.Send(new UpdateTextForArticlesCommand()
        {
            Data = dictionary
        }, cancellationToken);
    }
}