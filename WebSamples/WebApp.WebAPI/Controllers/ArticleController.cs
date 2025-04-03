using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Core.DTOs;
using WebApp.Services.Abstract;

namespace WebApp.WebAPI.Controllers
{
    /// <summary>
    /// Controller for articles
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly ILogger<ArticlesController> _logger;
        public ArticlesController(IArticleService articleService, ILogger<ArticlesController> logger)
        {
            _articleService = articleService;
            _logger = logger;
        }

        /// <summary>
        /// Get article by id
        /// </summary>
        /// <param name="id">Identifier of article</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Article by id</returns>
        [HttpGet("{id}")]
        [ProducesResponseType<ArticleDto>(statusCode:200)]
        [ProducesResponseType<NotFoundResult>(statusCode:404)]
        [ProducesResponseType<ErrorResponseModel>(statusCode:500)]
        public async Task<IActionResult> GetArticle(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var article = await _articleService.GetByIdAsync(id, cancellationToken);
                if (article == null)
                {
                    return NotFound();
                }
                return Ok(article);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting article by id");
                var errorModel = new ErrorResponseModel()
                {
                    Message = "Something goes wrong"
                };
                
                return StatusCode(StatusCodes.Status500InternalServerError, errorModel);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetArticles(int? minRate, int pageSize = 15, int pageNumber = 1, CancellationToken cancellationToken = default)
        {
            var articles = await _articleService.GetAllPositiveAsync(minRate, pageSize, pageNumber, cancellationToken);
            return Ok(articles);
        }
        
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(Guid id)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            //await _articleService.DeleteAsync(id);
            return Ok();
        }
    }
}
