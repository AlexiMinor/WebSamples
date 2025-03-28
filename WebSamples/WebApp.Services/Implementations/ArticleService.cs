using System.Collections.Concurrent;
using HtmlAgilityPack;
using MediatR;
using Microsoft.Extensions.Logging;
using WebApp.Core.DTOs;
using WebApp.Data.CQS.Commands;
using WebApp.Data.CQS.Queries;
using WebApp.Data.Entities;
using WebApp.Services.Abstract;
using WebApp.Services.Mappers;

namespace WebApp.Services.Implementations;

public class ArticleService : IArticleService
{
    private readonly IMediator _mediator;
    private readonly ILogger<ArticleService> _logger;
    private readonly ISourceService _sourceService;
    private readonly IHtmlRemoverService _htmlRemover;
    private readonly IRssService _rssService;
    private readonly IRateService _rateService;
    private readonly ArticleMapper _mapper;

    public ArticleService(ILogger<ArticleService> logger, IMediator mediator, ArticleMapper mapper, ISourceService sourceService, IRssService rssService, IRateService rateService, IHtmlRemoverService htmlRemover)
    {
        //_dbContext = dbContext;
        _logger = logger;
        _mediator = mediator;
        _mapper = mapper;
        _sourceService = sourceService;
        _rssService = rssService;
        _rateService = rateService;
        _htmlRemover = htmlRemover;
    }

    public async Task<ArticleDto?[]> GetAllPositiveAsync(double? minRate, int pageSize, int pageNumber, CancellationToken cancellationToken = default)
    {
        if (pageSize < 1 || pageNumber < 1)
        {
            _logger.LogWarning("Page size and number must be greater than 0");
            throw new ArgumentException("Page size and number must be greater than 0");
        }

        try
        {
            return (await _mediator.Send(new GetPositiveArticlesWithPaginationQuery()
                {
                    PositivityRate = minRate,
                    Page = pageNumber,
                    PageSize = pageSize
                }, cancellationToken))
                .Select(article => _mapper.ArticleToArticleDto(article))
                .ToArray();

        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while fetching articles");
            throw;
        }

        throw new ArgumentException("Page size and number must be greater than 0");
    }

    public async Task<ArticleDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _mapper.ArticleToArticleDto(await _mediator.Send(new GetArticleByIdQuery()
            {
                Id = id
            }, cancellationToken));
    }

    public async Task<int> CountAsync(double minRate, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(new GetArticlesCountByPositivityRateQuery()
        {
            PositivityRate = 0.0
        }, cancellationToken);
    }

    public async Task<string[]> GetUniqueArticlesUrls(CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(new GetUniqueArticlesUrlsQuery(), cancellationToken);
    }

    public async Task AddArticlesAsync(IEnumerable<Article> newUniqueArticles, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(new AddArticlesCommand() { Articles = newUniqueArticles }, cancellationToken);
    }

    public async Task AddArticleAsync(ArticleDto articleDto, CancellationToken cancellationToken = default)
    {
        var article = _mapper.ArticleDtoToArticle(articleDto);
        article.Id = Guid.NewGuid();

        await _mediator.Send(new AddArticleCommand()
        {
            Article = article
        }, cancellationToken);
    }

    public async Task AggregateArticleInfoFromSourcesByRssAsync(CancellationToken cancellationToken = default)
    {
        var sources = await _sourceService.GetSourceWithRssAsync(cancellationToken);
        var newArticles = new List<Article>();
        foreach (var source in sources)
        {
            var existedArticlesUrls = await GetUniqueArticlesUrls(cancellationToken);
            var articles = await _rssService.GetRssDataAsync(source.RssUrl, source.Id, cancellationToken);
            var newArticlesData = articles.Where(article => !existedArticlesUrls.Contains(article.Url)).ToArray();
            newArticles.AddRange(newArticlesData);
        }
        await AddArticlesAsync(newArticles, cancellationToken);
    }
    
    public async Task UpdateTextForArticlesByWebScrappingAsync(CancellationToken cancellationToken = default)
    {
        var ids = await _mediator.Send(new GetIdsForArticlesWithNoTextQuery(), cancellationToken);

        //TODO: Move to web scrapping service with source checking and algorithm for getting article content
        var dictionary = new Dictionary<Guid, string>();
        foreach (var id in ids)
        {
            var article = await _mediator.Send(new GetArticleByIdQuery()
            {
                Id = id
            }, cancellationToken);
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

    public async Task RateUnratedArticles(CancellationToken cancellationToken = default)
    {
        var articlesWithNoRate = await GetArticlesWithoutRate();
        var dictionary = new ConcurrentDictionary<Guid, double?>();

        await Parallel.ForEachAsync(articlesWithNoRate, cancellationToken, async (dto, token) =>
        {
            var contentForLegitimization = _htmlRemover.RemoveHtmlTags(dto.Content);
            var rate = await _rateService.GetRateAsync(contentForLegitimization, token);
            dictionary.TryAdd(dto.Id, rate);

        });
        //foreach (var article in articlesWithNoRate)
        //{
        //    var contentForLegitimization = _htmlRemover.RemoveHtmlTags(article.Content);
        //    var rate = await _rateService.GetRateAsync(contentForLegitimization, cancellationToken);
        //}
        
        await _mediator.Send(new UpdateRateForArticlesCommand() { 
            Data = dictionary
                .Where(pair => pair.Value.HasValue)
                .Select(pair => new KeyValuePair<Guid, double>(pair.Key, pair.Value.Value)).ToDictionary() }, 
            cancellationToken);
    }

    private async Task<ArticleDto[]> GetArticlesWithoutRate()
    {
        var articles = await _mediator.Send(new GetArticlesWithoutRateQuery());
        return articles.Select(article => _mapper.ArticleToArticleDto(article)).ToArray();
    }
}