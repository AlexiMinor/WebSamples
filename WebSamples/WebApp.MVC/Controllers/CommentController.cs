using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.MVC.Models;

namespace WebApp.MVC.Controllers
{
    public class CommentController : Controller
    {

        private static readonly List<Comment> Comments = [];

        [HttpPost]
        [Authorize]
        public IActionResult Create([FromBody]Comment comment)
        {
            comment.CreatedAt = DateTime.Now;
            comment.Id = Comments.Count + 1;
            Comments.Add(comment);
            return Ok();
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(Comments);
        }
    }
}
