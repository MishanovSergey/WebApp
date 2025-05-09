namespace WebApp.RabbitMqConsumer;

public interface IRabbitMqConsumerService
{
    Task<string> GetSingleMessageAsync(CancellationToken cancellationToken);
}
