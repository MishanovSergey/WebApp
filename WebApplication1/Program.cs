using WebApp.RabbitMqConsumer;
using WebApp.RabbitMqProducer;
using WebApp.Services;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<INewsService, NewsService>();
            builder.Services.AddSingleton<IRabbitMqProducerService, RabbitMqProducerService>();
            builder.Services.AddSingleton<IRabbitMqConsumerService, RabbitMqConsumerService>();
            builder.Services.AddSingleton<MessageStorageService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
