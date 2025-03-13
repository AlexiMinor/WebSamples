using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Data.Entities;
using WebApp.MVC.Filters;
using WebApp.MVC.Models;
using WebApp.Mvc.Models.Models;
using WebApp.Services.Abstract;
using WebApp.Services.Mappers;

namespace WebApp.MVC.Controllers
{
    [Authorize(Roles = "User, Admin")]
    public class ArticlesController : Controller
    {
        
        private Guid _sourceId = Guid.Parse("2c487898-f266-4d8f-8462-b482787ce60c");

        private readonly IArticleService _articleService;
        private readonly ISourceService _sourceService;
        private readonly IRssService _rssService;
        private readonly ArticleMapper _articleMapper;
        private readonly ILogger<ArticlesController> _logger;
        public ArticlesController(IArticleService articleService, 
                ILogger<ArticlesController> logger, 
                ISourceService sourceService, 
                IRssService rssService, 
                ArticleMapper articleMapper)
        //ArticleAggregatorContext articleAggregatorContext
        {
            _articleService = articleService;
            _logger = logger;
            _sourceService = sourceService;
            _rssService = rssService;
            _articleMapper = articleMapper;
            //_articleAggregatorContext = articleAggregatorContext;//
        }

        [HttpGet]
        [WhiteSpaceRemover]
        public async Task<IActionResult> Index(PaginationModel pageData)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var list = ModelState.Select(item => item.Key).ToList();
                    _logger.LogWarning("Invalid model state", list);    
                    return BadRequest(list);
                }
                const double baseMinRate = 0;
                var articles = (await _articleService.GetAllPositiveAsync(baseMinRate, pageData.PageSize, pageData.PageNumber))//will be replaced with mapper in future
                    .Select(article => _articleMapper.ArticleDtoToArticleModel(article))
                    .ToArray();
                _logger.LogInformation("Articles fetched successfully");

                var totalArticlesCount = await _articleService.CountAsync(baseMinRate);
                var pageInfo = new PageInfo()
                {
                    PageNumber = pageData.PageNumber,
                    PageSize = pageData.PageSize,
                    TotalItems = totalArticlesCount
                };

                return View(new ArticleCollectionModel
                {
                    Articles = articles,
                    PageInfo = pageInfo
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }

        }

        [HttpGet]
        public async Task<IActionResult> Details([FromRoute] Guid id)
        {
            var article = await _articleService.GetByIdAsync(id);
            if (article != null)
            {
                var model = _articleMapper.ArticleDtoToArticleModel(article);
                return View(model);
            }

            return NotFound();
        }

        //[HttpGet]
        //public async Task<IActionResult> Details2([FromRoute] Guid id)
        //{
        //    //var article = await _articleService.GetByIdAsync(id);
        //    //if (article != null)
        //    //{

        //    //    var model = _articleMapper.ArticleToArticleModel(article);
        //    //    return View(model);
        //    //}

        //    //return NotFound();
        //}

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Aggregate()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AggregateProcessing(CancellationToken cancellationToken=default)
        {
            //1. Get all sources
            var sources = await _sourceService.GetSourceWithRssAsync();
            var newArticles = new List<Article>();
            foreach (var source in sources)
            {
                var existedArticlesUrls = await _articleService.GetUniqueArticlesUrls(cancellationToken);
                var articles = await _rssService.GetRssDataAsync(source,cancellationToken);
                var newArticlesData = articles.Where(article => !existedArticlesUrls.Contains(article.Url)).ToArray();
                newArticles.AddRange(newArticlesData);
            }
            await _articleService.AddArticlesAsync(newArticles,cancellationToken);

            await _articleService.UpdateTextForArticlesByWebScrappingAsync(cancellationToken);
            //3. web scraping for each article
            //4. rate each article
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddArticleModel()
            {
                Sources = new SelectList(await _sourceService.GetSourceWithRssAsync(), nameof(Source.Id), nameof(Source.Name))
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProcessing(AddArticleModel model)
        {
            if (!ModelState.IsValid)
            {
                //ModelState.AddModelError();
                var errors = ModelState.SelectMany(pair => pair.Value.Errors)
                    .ToArray();

                return View("Add", model);
            }


            var dto = _articleMapper.AddArticleModelToArticleDto(model);

            await _articleService.AddArticleAsync(dto);
            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public async Task<IActionResult> Edit(Guid id)
        //{
        //    var article = await _articleService.GetByIdAsync(id);
        //    if (article != null)
        //    {
        //        var model = new ArticleModel()
        //        {
        //            Id = article.Id,
        //            Title = article.Title,
        //            Description = article.Description,
        //            Source = article.Source.Name,
        //            CreationDate = article.CreationDate,
        //            Rate = article.PositivityRate ?? 0
        //        };
        //        return View(model);
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> Edit(ArticleModel model)
        {
            var data = model;
            return Ok();
        }

       
    }
}
