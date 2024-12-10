using WebApp.DTO;

namespace WebApp.Services;

public class ProgrammerService : IProgrammerService
{
    private static readonly string[] Excuses =
    [
        "Устал", "Работы много", "Голова болит", "Макс, ну че ты", "В бар пошли", "Уснул", "Завтра точно начну", "На шару устроюсь", "Элеонора мешает"
    ];

    public ProgrammerService()
    {
        
    }

    public List<LazyProgrammer> GetLazyProgrammers()
    {
        var result = Enumerable.Range(1, 5).Select(index => new LazyProgrammer
        {
            Date = DateOnly.FromDateTime(new DateTime(2024, 1, 1).AddDays(Random.Shared.Next(366))),
            CurrentSalary = Random.Shared.Next(10, 15) * 10,
            Excuse = Excuses[Random.Shared.Next(Excuses.Length)]
        })
        .ToList();

        return result;
    }
}
