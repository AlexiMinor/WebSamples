using Microsoft.AspNetCore.Mvc;
using WebApp.MVC.Models;
using WebApp.Services.Samples;

namespace WebApp.MVC.Controllers
{
    //[NonController]
    public class SampleController : Controller
    {
        private readonly ITestService _testService;
        private readonly IScopedService _scopedService;
        private readonly ITransientService _transientService;

        public SampleController(ITestService testService, 
            IScopedService scopedService, 
            ITransientService transientService)
        {
            _testService = testService;
            _scopedService = scopedService;
            _transientService = transientService;
        }

        public IActionResult THS()
        {
            return View();
        }
        public IActionResult Index()
        {
            var data = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            return View(data);
        }
        public IActionResult Test()
        {
            _transientService.Do();
            _scopedService.Do();
            _testService.Do();
            
            Console.WriteLine("second time:");
            _transientService.Do();
            _scopedService.Do();
            _testService.Do();

            return Ok();
        }
        //public string Index(Client client)
        //{
        //    if (Request.Query.ContainsKey("SecretCode"))
        //    {
        //        return "You know secret code";
        //    }
        //    return string.IsNullOrWhiteSpace(client.FirstName) && string.IsNullOrWhiteSpace(client.LastName)
        //        ? "Hello world" 
        //        : $"Hello {client.FirstName} {client.LastName}";
        //}

        private IActionResult NonAction()
        {
            return Ok();
        }

        //[NonAction]
        public IActionResult Hello(string name)
        {
            ViewData["Name"] = "Bob";
            ViewData["AAA"] = new[] {1,2,3};

            ViewBag.Name = "Alice";
            ViewBag.HelloMessage = "Hello ";
            ViewBag.SomeData = new[] { 1, 2, 3 };

            return View();
            //return View(new HelloModel(){Name = name});//WebApi
        }

        //[ActionName("GoB")]
        //public IActionResult GoA()
        //{
        //    return Ok("1");
        //}

        [HttpGet]
        public IActionResult Get()
        {
            //return new ContentResult();
            //return Content("Some string"); 
            //return new EmptyResult();//100% analogue of void -> 200 OK 
            //return NoContent(); //204
            //return File(); ->the most general file for download
            //return new FileContentResult(); -> byte array
            //return new FileStreamResult(); -> File stream
            //return new ObjectResult(new {A=1,B=15}); almost never used
            //return Ok();//potentially any object there
            //return StatusCode(415);
            //return Unauthorized();//401
            //return NotFound();//404
            //return new NotFoundObjectResult();
            //return BadRequest("Bad request"); //400
            //return Created(); //201
            //return Accepted();//202
            //return Json();//almost never used, replaced OK
            //return PartialView();
            //return View(); //return new ViewResult() => return prepared View();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        //[HttpHead]
        //[HttpDelete]
        //[HttpPut]
        //[HttpPatch]
        //[HttpOptions]
        public IActionResult Post()
        {
            return Ok("post");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public IActionResult LoginProcess(LoginModel model)
        {
            return Ok(model);
        }
    }
}
