using WebApp.DTO;

namespace WebApp.Services;

public interface INewsService
{
    Task<List<NewsInfo>> GetNewsInfoAsync();
}
