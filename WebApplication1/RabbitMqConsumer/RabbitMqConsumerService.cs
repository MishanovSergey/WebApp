using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace WebApp.RabbitMqConsumer
{
    public class RabbitMqConsumerService : IRabbitMqConsumerService
    {
        public async Task<string> GetMessage()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672,
            };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "MyQueue", durable: false, exclusive: false, autoDelete: false,
            arguments: null);

            StringBuilder result = new();

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                result.AppendLine($" [x] Received {message}");
                Console.WriteLine($" [x] Received {message}");

                //int dots = message.Split('.').Length - 1;
                //await Task.Delay(dots * 1000);

                await Task.Yield();
            };

            await channel.BasicConsumeAsync("MyQueue", autoAck: true, consumer: consumer);

            return result.ToString();
        }
    }
}
