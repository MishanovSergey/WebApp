﻿using AngleSharp;
using WebApp.DTO;

namespace WebApp.Services;

public class NewsService : INewsService
{
    public NewsService()
    {

    }

    public async Task<List<NewsInfo>> GetNewsInfoAsync()
    {
        var address = "https://lenta.ru";
        var config = Configuration.Default.WithDefaultLoader();
        var document = await BrowsingContext.New(config).OpenAsync(address);
        List<NewsInfo> listNewsInfo = [];

        //Получаем объекты новостей
        var newsItems = document.QuerySelectorAll("a")
            .Where(item => item.ClassName != null && (item.ClassName.Contains("card-mini") || 
                           item.ClassName.Contains("card-big") ||
                           item.ClassName.Contains("card-feature")));

        foreach (var newsItem in newsItems)
        {
            //Получаем заголовок
            string newsTitle = "";

            if(newsItem.QuerySelector("span") != null)  //Заголовок объекта состоит из двух объектов разных классов
            {
                    newsTitle =
                    $"{newsItem.QuerySelectorAll("h3")
                        .FirstOrDefault(item => item.ClassName != null && (item.ClassName.Contains("card-big__title") ||
                                       item.ClassName.Contains("card-feature__title") ||
                                       item.ClassName.Contains("card-mini__title")))!.TextContent}" +
                    $"{newsItem.QuerySelectorAll("span")
                        .FirstOrDefault(item => item.ClassName != null && (item.ClassName.Contains("card-big__rightcol") ||
                                        item.ClassName.Contains("card-feature__rightcol")||
                                        item.ClassName.Contains("advertisable-plate__tooltip-content")))!.TextContent}";
            }
            else
            {
                newsTitle = newsItem.QuerySelectorAll("h3")
                    .FirstOrDefault(item => item.ClassName != null && (item.ClassName.Contains("card-mini__title") ||
                                    item.ClassName.Contains("card-big__title") ||
                                    item.ClassName.Contains("card-feature__title")))!.TextContent;
            }

            //Получаем время публикации
            var newsPostTime = newsItem.QuerySelectorAll("time")
                    .FirstOrDefault(item => item.ClassName != null && (item.ClassName.Contains("card-mini__info-item") || 
                                item.ClassName.Contains("card-big__date") ||
                                item.ClassName.Contains("card-feature__date")));

            //Заполняем список NewsInfo связанными заголовками и публикациями
            listNewsInfo.Add(new NewsInfo 
            { 
                Header = newsTitle.Trim(),
                PostTime = newsPostTime?.TextContent ?? "Время не указано",
            });
        }

        return listNewsInfo;
    }
}
