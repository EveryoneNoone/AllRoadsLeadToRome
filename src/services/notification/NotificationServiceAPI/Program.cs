using Application;
using Infrustructure;
using MassTransit;
using NotificationServiceAPI;
using NotificationServiceAPI.Consumers;
using NotificationServiceAPI.Settings;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.        
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Add MassTransit and configure RabbitMQ
        builder.Services.AddMassTransit(x =>
        {
            x.AddConsumer<ConsumerMessage>();
            x.UsingRabbitMq((context, cfg) =>
            {
                var rabbitMqSettings = builder.Configuration.GetSection("RMQSettings").Get<RmqSettings>();
                cfg.Host(new Uri($"rabbitmq://{rabbitMqSettings.Host}"), h =>
                {
                    h.Username(rabbitMqSettings.Login);
                    h.Password(rabbitMqSettings.Password);
                });
                cfg.ReceiveEndpoint("masstransit_event_queue_1", e =>
                {
                    e.ConfigureConsumer<OrderStatusChangedConsumer>(context);
                    e.UseMessageRetry(r =>
                    {
                        r.Incremental(3, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
                    });
                });
            });
        });

        //builder.Services.AddHostedService<HostService>();

        // Configure MongoDB settings
        builder.Services.Configure<NotificationDatabaseSettings>(builder.Configuration.GetSection("NotificationDatabaseSettings"));
        builder.Services.AddSingleton<MongoDBService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        //if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        //app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }

    ///// <summary>
    ///// Configure RabbitMQ settings
    ///// </summary>
    ///// <param name="configurator"></param>
    ///// <param name="configuration"></param>
    //private static void ConfigureRMQ(IRabbitMqBusFactoryConfigurator configurator, IConfiguration configuration)
    //{
    //    var rabbitMqSettings = configuration.GetSection("RMQSettings").Get<RmqSettings>();
    //    configurator.Host(new Uri($"rabbitmq://{rabbitMqSettings.Host}"), h =>
    //    {
    //        h.Username(rabbitMqSettings.Login);
    //        h.Password(rabbitMqSettings.Password);
    //    });
    //}

    //private static void RegisterEndpoints(IRabbitMqBusFactoryConfigurator configurator)
    //{
    //    configurator.ReceiveEndpoint("masstransit_event_queue_1", e =>
    //    {
    //        e.Consumer<ConsumerMessage>();
    //        e.UseMessageRetry(r =>
    //        {
    //            r.Incremental(3, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
    //        });
    //    });
    //}
}
