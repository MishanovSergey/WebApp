using Microsoft.AspNetCore.Mvc;
using WebApp.DTO;
using WebApp.Services;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LazyProgrammerController : ControllerBase
    {
        private readonly IProgrammerService _programmerService;
        private readonly ILogger<LazyProgrammerController> _logger;

        public LazyProgrammerController(IProgrammerService programmerService, ILogger<LazyProgrammerController> logger)
        {
            _programmerService = programmerService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<LazyProgrammer>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public ActionResult Get()
        {
            try
            {
                var result = _programmerService.GetLazyProgrammersAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
