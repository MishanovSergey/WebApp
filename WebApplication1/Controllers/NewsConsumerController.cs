using Microsoft.AspNetCore.Mvc;
using WebApp.RabbitMqConsumer;

namespace WebApp.Controllers
{
    [ApiController]
[Route("api/messages")]
public class MessagesController : ControllerBase
{
    private readonly IRabbitMqConsumerService _consumerService;

    public MessagesController(IRabbitMqConsumerService consumerService)
    {
        _consumerService = consumerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetMessage(CancellationToken cancellationToken)
    {
        try
        {
            var message = await _consumerService.GetSingleMessageAsync(cancellationToken);
            return Ok(message);
        }
        catch (OperationCanceledException)
        {
            return StatusCode(499);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
}
