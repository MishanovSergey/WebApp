using WebApp.DTO;

namespace WebApp.Services;

public interface IProgrammerService
{
    Task<List<LazyProgrammer>> GetLazyProgrammersAsync();
}
