using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using WebApp.Services;

namespace WebApp.RabbitMqConsumer
{
    public class RabbitMqConsumerService : IRabbitMqConsumerService, IAsyncDisposable
    {
        private readonly MessageStorageService _messageStorage;
        private IConnection _connection;
        private IChannel _channel;

        public RabbitMqConsumerService(MessageStorageService messageStorage)
        {
            _messageStorage = messageStorage;
        }

        public async Task InitializeAsync()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672,
                ConsumerDispatchConcurrency = 1
            };

            _connection = await factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();

            await _channel.QueueDeclareAsync(
                queue: "MyQueue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }
        public async Task<string> GetSingleMessageAsync(CancellationToken cancellationToken = default) 
        {
            if (_channel == null) await InitializeAsync();

            var tcs = new TaskCompletionSource<string>();

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                try
                {
                    var message = Encoding.UTF8.GetString(ea.Body.Span);
                    _messageStorage.Messages.Enqueue(message);
                    Console.WriteLine($"Received message: {message}");

                    await _channel.BasicAckAsync(ea.DeliveryTag, false);
                    tcs.TrySetResult(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex}, Error processing message");
                    await _channel.BasicNackAsync(ea.DeliveryTag, false, false);
                    tcs.TrySetException(ex);
                }
            };

            string consumerTag = await _channel.BasicConsumeAsync(
                queue: "MyQueue",
                autoAck: false,
                consumer: consumer);

            // Ждем получения одного сообщения или отмены
            using (cancellationToken.Register(() => tcs.TrySetCanceled()))
            {
                try
                {
                    var result = await tcs.Task;
                    await _channel.BasicCancelAsync(consumerTag);
                    return result;
                }
                catch (OperationCanceledException)
                {
                    await _channel.BasicCancelAsync(consumerTag);
                    throw;
                }
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_channel != null)
                await _channel.CloseAsync();

            if (_connection != null)
                await _connection.CloseAsync();
        }
    }
}
