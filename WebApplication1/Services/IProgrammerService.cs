using WebApp.DTO;

namespace WebApp.Services;

public interface IProgrammerService
{
    List<LazyProgrammer> GetLazyProgrammers();
}
