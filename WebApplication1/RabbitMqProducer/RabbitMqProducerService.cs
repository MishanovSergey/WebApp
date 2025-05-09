using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace WebApp.RabbitMqProducer
{
    public class RabbitMqProducerService : IRabbitMqProducerService
    {
        public void SendMessage(object obj)
        {
            var message = JsonSerializer.Serialize(obj);
            SendMessage(message);
        }

        public async void SendMessage(string message)
        {
            var factory = new ConnectionFactory() 
            { 
                HostName = "localhost",
                Port = 5672,
            };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();
            {
                await channel.QueueDeclareAsync(queue: "MyQueue",
                                durable: true,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                await channel.BasicPublishAsync(exchange: "",
                                    routingKey: "MyQueue",
                                    mandatory: true,
                                    body: body);
            }
        }
    }
}