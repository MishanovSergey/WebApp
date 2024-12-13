using Microsoft.AspNetCore.Mvc;
using WebApp.DTO;
using WebApp.Services;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsInfoController : ControllerBase
    {
        private readonly INewsService _programmerService;
        private readonly ILogger<NewsInfoController> _logger;

        public NewsInfoController(INewsService programmerService, ILogger<NewsInfoController> logger)
        {
            _programmerService = programmerService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<NewsInfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Get()
        {
            try
            {
                var result = await _programmerService.GetNewsInfoAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}