namespace WebApp.RabbitMqProducer;

public interface IRabbitMqProducerService
{
    void SendMessage(object obj);
    void SendMessage(string message);
}
