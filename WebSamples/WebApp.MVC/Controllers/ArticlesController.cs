using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Data.Entities;
using WebApp.MVC.Filters;
using WebApp.MVC.Models;
using WebApp.Services.Abstract;

namespace WebApp.MVC.Controllers
{
    public class ArticlesController : Controller
    {
        private Guid _sourceId = Guid.Parse("2c487898-f266-4d8f-8462-b482787ce60c");
        private readonly IArticleService _articleService;
        //private readonly ArticleAggregatorContext _articleAggregatorContext;//do not use directly in controller
        public ArticlesController(IArticleService articleService)
            //ArticleAggregatorContext articleAggregatorContext
        {
            _articleService = articleService;
            //_articleAggregatorContext = articleAggregatorContext;//
        }

        [HttpGet]
        [WhiteSpaceRemover]
        public async Task<IActionResult> Index(PaginationModel pageData)
        {
            if (!ModelState.IsValid)
            {
                var list = new List<string>();
                foreach (var item in ModelState)
                {
                    list.Add(item.Key);
                }
                return BadRequest(list);
            }
            const double baseMinRate = 0;
            var articles = (await _articleService.GetAllPositiveAsync(baseMinRate, pageData.PageSize, pageData.PageNumber))//will be replaced with mapper in future
                .Select(article => new ArticleModel()
                {
                    Id = article.Id,
                    Title = article.Title,
                    Description = article.Description,
                    Source = article.Source.Name,
                    CreationDate = article.CreationDate,
                    Rate = article.PositivityRate ?? 0
                })
                .ToArray();
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

        [HttpGet]
        public async Task<IActionResult> Details([FromRoute]Guid id)
        {
            var article = await _articleService.GetByIdAsync(id);
            if (article!= null)
            {
                var model = new ArticleModel()
                {
                    Id = article.Id,
                    Title = article.Title,
                    Description = article.Description,
                    Source = article.Source.Name,
                    CreationDate = article.CreationDate,
                    Rate = article.PositivityRate ?? 0
                };
                return View(model);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Details2([FromRoute] Guid id)
        {
            var article = await _articleService.GetByIdAsync(id);
            if (article != null)
            {
                var model = new ArticleModel()
                {
                    Id = article.Id,
                    Title = article.Title,
                    Description = article.Description,
                    Source = article.Source.Name,
                    CreationDate = article.CreationDate,
                    Rate = article.PositivityRate ?? 0
                };
                return View(model);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Add(AddArticleModel? model)
        {
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
            
            //_articleAggregatorContext.Articles.blablalbla
            var article = new Article()
            {
                Id = Guid.NewGuid(),
                Title = model.Title,
                Description = model.Description,
                PositivityRate = model.Rate,
                Content = "",
                Url = "",
                CreationDate = DateTime.Now,
                SourceId = _sourceId
            };
            _articleService.AddArticleAsync(article);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var article = await _articleService.GetByIdAsync(id);
            if (article != null)
            {
                var model = new ArticleModel()
                {
                    Id = article.Id,
                    Title = article.Title,
                    Description = article.Description,
                    Source = article.Source.Name,
                    CreationDate = article.CreationDate,
                    Rate = article.PositivityRate ?? 0
                };
                return View(model);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ArticleModel model)
        {
            var data = model;
            return Ok();
        }
    }
}
