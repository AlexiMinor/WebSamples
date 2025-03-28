using System.Threading;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data.Entities;
using WebApp.Services.Abstract;
using WebApp.Services.Implementations;

namespace WebApp.WebAPI.Controllers
{
    /// <summary>
    /// Provides API endpoint for aggregating articles.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesAggregationController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly ISourceService _sourceService;
        private readonly IRssService _rssService;

        public ArticlesAggregationController(IArticleService articleService, ISourceService sourceService, IRssService rssService)
        {
            _articleService = articleService;
            _sourceService = sourceService;
            _rssService = rssService;
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AggregateArticles(CancellationToken cancellationToken = default)
        {
           RecurringJob.AddOrUpdate("RssParser", 
               () => _articleService.AggregateArticleInfoFromSourcesByRssAsync(cancellationToken),
               "0 * * * *");//cron better be set in appsettings

           RecurringJob.AddOrUpdate("WebScrapper",
               () => _articleService.UpdateTextForArticlesByWebScrappingAsync(cancellationToken),
               "15 * * * *");//cron better be set in appsettings

            //3. web scraping for each article
            //4. rate each article
            return Ok();
        }
    }
}
