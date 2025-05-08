using Microsoft.AspNetCore.Mvc;
using WebApp.DTO;
using WebApp.Services;
using WebApp.RabbitMqProducer;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsInfoController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly IRabbitMqProducerService _mqService;
        private readonly ILogger<NewsInfoController> _logger;

        //[Route("[action]/{message}")]
        //[HttpGet]
        public NewsInfoController(INewsService programmerService, IRabbitMqProducerService mqService, ILogger<NewsInfoController> logger)
        {
            _newsService = programmerService;
            _mqService = mqService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<NewsInfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Get()
        {
            try
            {
                var result = await _newsService.GetNewsInfoAsync();
                var newsForMessage = result.FirstOrDefault();
                string message = $"{newsForMessage?.Header}. {newsForMessage?.PostTime}";
                _mqService.SendMessage(message);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
