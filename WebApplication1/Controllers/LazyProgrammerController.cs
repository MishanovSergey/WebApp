using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LazyProgrammerController : ControllerBase
    {
        private static readonly string[] Excuses = new[]
        {
            "Устал", "Работы много", "Голова болит", "Макс, ну че ты", "В бар пошли", "Уснул", "Завтра точно начну", "На шару устроюсь", "Элеонора мешает"
        };

        private readonly ILogger<LazyProgrammerController> _logger;

        public LazyProgrammerController(ILogger<LazyProgrammerController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetLazyProgrammer")]
        public IEnumerable<LazyProgrammer> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new LazyProgrammer
            {
                Date = DateOnly.FromDateTime(new DateTime(2024, 1, 1).AddDays(Random.Shared.Next(366))),
                CurrentSalary = Random.Shared.Next(10, 15)*10,
                Excuse = Excuses[Random.Shared.Next(Excuses.Length)]
            })
            .ToArray();
        }
    }
}
