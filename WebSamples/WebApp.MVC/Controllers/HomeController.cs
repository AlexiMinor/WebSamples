using System.Diagnostics;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data.CQS.Commands;
using WebApp.MVC.Filters;
using WebApp.MVC.Models;
using WebApp.Services.Abstract;

namespace WebApp.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;
        private readonly IArticleService _articleService;
        
        public HomeController(ILogger<HomeController> logger, 
            IConfiguration configuration, IMediator mediator, IArticleService articleService)
        {
            _logger = logger;
            _configuration = configuration;
            _mediator = mediator;
            _articleService = articleService;
        }

        [CustomResourceFilter]
        [WhiteSpaceRemover]
        public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
        {
      
            var code = _configuration["Settings:SecretCode"];
            //todo should be moved to correct way
             await _mediator.Send(new TryToCreateRolesIfNecessaryCommand());
            //var confSection = _configuration.GetSection("Settings");
            //var child = _configuration.GetChildren();
            RecurringJob.AddOrUpdate("RssParser",
                () => _articleService.AggregateArticleInfoFromSourcesByRssAsync(cancellationToken),
                "0 * * * *");//cron better be set in appsettings

            RecurringJob.AddOrUpdate("WebScrapper",
                () => _articleService.UpdateTextForArticlesByWebScrappingAsync(cancellationToken),
                "15 * * * *");//cron better be set in appsettings

            //IConfiguration[key]
            return View("Index",code);
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        //public IActionResult Sample()
        //{
        //    return Ok();

        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
