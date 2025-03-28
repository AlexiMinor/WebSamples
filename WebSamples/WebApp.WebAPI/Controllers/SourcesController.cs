using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data.Entities;
using WebApp.Services.Abstract;

namespace WebApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SourcesController : ControllerBase
    {
        private readonly ISourceService _sourceService;
        private readonly ILogger<SourcesController> _logger;
        public SourcesController(ISourceService sourceService, ILogger<SourcesController> logger)
        {
            _sourceService = sourceService;
            _logger = logger;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSourceById(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var source = await _sourceService.GetByIdAsync(id, cancellationToken);
                if (source == null)
                {
                    return NotFound();
                }
                return Ok(source);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting source by id");
                return StatusCode(StatusCodes.Status500InternalServerError, "Something goes wrong");
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> GetSources(CancellationToken cancellationToken = default)
        {
            var sources= await _sourceService.GetSourceWithRssAsync(cancellationToken);
            return Ok(sources);
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
