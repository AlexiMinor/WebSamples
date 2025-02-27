using System.Diagnostics;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data.CQS.Commands;
using WebApp.MVC.Filters;
using WebApp.MVC.Models;

namespace WebApp.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;
        
        public HomeController(ILogger<HomeController> logger, 
            IConfiguration configuration, IMediator mediator)
        {
            _logger = logger;
            _configuration = configuration;
            _mediator = mediator;
        }

        [CustomResourceFilter]
        [WhiteSpaceRemover]
        public async Task<IActionResult> Index()
        {
            var code = _configuration["Settings:SecretCode"];
            //todo should be moved to correct way
             await _mediator.Send(new TryToCreateRolesIfNecessaryCommand());
            //var confSection = _configuration.GetSection("Settings");
            //var child = _configuration.GetChildren();
            
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
