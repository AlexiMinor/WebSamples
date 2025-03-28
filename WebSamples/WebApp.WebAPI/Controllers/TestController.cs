using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Services.Abstract;

namespace WebApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly IArticleService _articleService;
        public TestController(ILogger<TestController> logger, IArticleService articleService)
        {
            _logger = logger;
            _articleService = articleService;
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateJobs()
        //{
        //    var id = BackgroundJob.Enqueue(() => Console.WriteLine("Hello there"));
        //    var delayedJobId =
        //        BackgroundJob.Schedule(() => Console.WriteLine($"Hello from {DateTime.Now.ToShortTimeString()}"),
        //            TimeSpan.FromMinutes(2));



        //    var chainJob1 = BackgroundJob.Enqueue(() => Console.WriteLine("1"));
        //    var chainJob2 = BackgroundJob.ContinueJobWith(chainJob1, () => Console.WriteLine("2"));
        //    var chainJob3 = BackgroundJob.ContinueJobWith(chainJob2, () => Console.WriteLine("3"));

        //    return Ok();
        //}

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await _articleService.RateUnratedArticles();
            return Ok();
        }
        
    }
}
