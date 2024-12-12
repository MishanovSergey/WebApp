using AngleSharp;
using AngleSharp.Dom;
using WebApp.DTO;
using AngleSharp;
using AngleSharp.Html.Parser;
using static System.Net.Mime.MediaTypeNames;

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

    public async Task<List<LazyProgrammer>> GetLazyProgrammersAsync()
    {
        //var config = Configuration.Default.WithDefaultLoader();
        var address = "https://lenta.ru";
        //var parser = new HtmlParser();
        //var document = parser.ParseDocument(address);

        // Setup the configuration to support document loading
        var config = Configuration.Default.WithDefaultLoader();
        // Load the names of all The Big Bang Theory episodes from Wikipedia
        //var address = "http://en.wikipedia.org/wiki/List_of_The_Big_Bang_Theory_episodes";
        // Asynchronously get the document
        var document = await BrowsingContext.New(config).OpenAsync(address);





        //var document = await BrowsingContext.New(config).OpenAsync(address);
        //string text = document.QuerySelector("time").Text();
        //Для хранения заголовков
        List<string> list = new List<string>();
        //Здесь мы получаем заголовки
        //var items = document.All.Where(m => m.LocalName == "h3" && m.ClassList.Contains("card-big__title"));
        IEnumerable<IElement> items = document.QuerySelectorAll("h3")
            .Where(item => item.ClassName != null && item.ClassName.Contains("card-big__title"));

        foreach (var item in items)
        {
            //Добавляем заголовки в коллекцию.
            list.Add(item.TextContent);
        }
        List<LazyProgrammer> listResult = new List<LazyProgrammer>();

        foreach (var item in list)
        {
            listResult.Add(new LazyProgrammer
            {
                Date = DateOnly.FromDateTime(new DateTime(2024, 1, 1).AddDays(Random.Shared.Next(366))),
                CurrentSalary = Random.Shared.Next(10, 15) * 10,
                Excuse = item//Excuses[Random.Shared.Next(Excuses.Length)]
            });
        }
        //var result = Enumerable.Range(1, list.Count).Select(index => new LazyProgrammer
        //{
        //    Date = DateOnly.FromDateTime(new DateTime(2024, 1, 1).AddDays(Random.Shared.Next(366))),
        //    CurrentSalary = Random.Shared.Next(10, 15) * 10,
        //    Excuse = text//Excuses[Random.Shared.Next(Excuses.Length)]
        //})
        //.ToList();

        return listResult;
    }
}
