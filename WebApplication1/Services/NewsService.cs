using AngleSharp;
using AngleSharp.Dom;
using WebApp.DTO;
using AngleSharp;
using AngleSharp.Html.Parser;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.Intrinsics.X86;
using System.Collections.Generic;

namespace WebApp.Services;

public class NewsService : INewsService
{
    public NewsService()
    {

    }

    public async Task<List<NewsInfo>> GetNewsInfoAsync()
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
        List<string> listHeaders = new List<string>();
        List<string> listPostTime = new List<string>();
        List<(string, string)> listResult = new List<(string, string)>();
        //Здесь мы получаем заголовки
        //var items = document.All.Where(m => m.LocalName == "h3" && m.ClassList.Contains("card-big__title"));
        IEnumerable<IElement> headers = document.QuerySelectorAll("h3")
            .Where(item => item.ClassName != null && (item.ClassName.Contains("card-big__title")));// || item.ClassName.Contains("card-mini__title")));
        IEnumerable<IElement> timeList = document.QuerySelectorAll("time")
            .Where(item => item.ClassName != null && (item.ClassName.Contains("card-big__date")));// || item.ClassName.Contains("card-mini__info-item")));

        foreach (var item in headers)
        {
            //Добавляем заголовки в коллекцию.
            listHeaders.Add(item.TextContent);
        }
        foreach (var item in timeList)
        {
            //Добавляем заголовки в коллекцию.
            listPostTime.Add(item.TextContent);
        }

        int counter = 0;
        foreach (var item in listHeaders) //New
        {
            listResult.Add((item, listPostTime[counter]));
            counter++;
        }

        List<NewsInfo> listLazyProgrammers = new List<NewsInfo>();

        foreach (var item in listResult.Distinct())
        {
            listLazyProgrammers.Add(new NewsInfo
            {
                PostTime = item.Item2,
                Header = item.Item1//Excuses[Random.Shared.Next(Excuses.Length)]
            });
        }
        //var result = Enumerable.Range(1, list.Count).Select(index => new LazyProgrammer
        //{
        //    Date = DateOnly.FromDateTime(new DateTime(2024, 1, 1).AddDays(Random.Shared.Next(366))),
        //    CurrentSalary = Random.Shared.Next(10, 15) * 10,
        //    Excuse = text//Excuses[Random.Shared.Next(Excuses.Length)]
        //})
        //.ToList();

        return listLazyProgrammers;
    }
}
