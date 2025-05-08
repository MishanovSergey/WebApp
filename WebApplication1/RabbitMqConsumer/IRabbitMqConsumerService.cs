namespace WebApp.RabbitMqConsumer;

public interface IRabbitMqConsumerService
{
    Task<string> GetMessage();
}
