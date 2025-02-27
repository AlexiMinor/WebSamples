using Microsoft.AspNetCore.Mvc;

namespace WebApp.MVC.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
