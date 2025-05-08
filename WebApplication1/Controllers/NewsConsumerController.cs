using Microsoft.AspNetCore.Mvc;
using WebApp.RabbitMqConsumer;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsConsumerController : ControllerBase
    {
        private readonly IRabbitMqConsumerService _mqService;
        private readonly ILogger<NewsInfoController> _logger;

        public NewsConsumerController(IRabbitMqConsumerService mqService, ILogger<NewsInfoController> logger)
        {
            _mqService = mqService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Get()
        {
            try
            {
                var result = await _mqService.GetMessage();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
