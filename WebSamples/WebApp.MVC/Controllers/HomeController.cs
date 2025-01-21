using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.MVC.Models;

namespace WebApp.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        
        public HomeController(ILogger<HomeController> logger, 
            IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var code = _configuration["Settings:SecretCode"];
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
