using MassTransit;
using NotificationServiceAPI;
using NotificationServiceAPI.Consumers;
using NotificationServiceAPI.Settings;

internal class Program
{
    private static void Main(string[] args)
    {
        //CreateHostBuilder(args).Build().Run();

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.        

        builder.Services.AddControllers();
        //Learn more about configuring Swagger / OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        builder.Services.AddMassTransit(x =>
        {
            x.AddConsumer<ConsumerMessage>();
            x.UsingRabbitMq((context, cfg) =>
            {
                ConfigureRMQ(cfg, builder.Configuration);
                RegisterEndpoints(cfg);
            });
        });
        builder.Services.AddHostedService<HostService>();

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

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureServices(services =>
                {
                    services.AddControllers();
                    services.AddEndpointsApiExplorer();
                    services.AddSwaggerGen();
                })
                .Configure((hostContext, app) =>
                {
                    if (hostContext.HostingEnvironment.IsDevelopment())
                    {
                        app.UseSwagger();
                        app.UseSwaggerUI();
                    }
                    app.UseHttpsRedirection();
                    app.UseAuthorization();
                    app.Build();
                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers();
                    });
                });
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddMassTransit(x =>
                {
                    x.AddConsumer<ConsumerMessage>();
                    x.UsingRabbitMq((context, cfg) =>
                    {
                        ConfigureRMQ(cfg, configuration);
                        RegisterEndpoints(cfg);
                    });
                });
                services.AddHostedService<HostService>();
            });
    }

    /// <summary>
    /// Для докера host использовать "host.docker.internal"
    /// </summary>
    /// <param name="configurator"></param>
    /// <param name="configuration"></param>
    private static void ConfigureRMQ(IRabbitMqBusFactoryConfigurator configurator, IConfiguration configuration)
    {
        var settings = configuration.Get<ApplicationSettings>().RmqSettings;
        configurator.Host(settings.Host,
            settings.VHost,
            h =>
            {
                h.Username(settings.Login);
                h.Password(settings.Password);
            });
    }

    private static void RegisterEndpoints(IRabbitMqBusFactoryConfigurator configurator)
    {
        configurator.ReceiveEndpoint($"masstransit_event_queue_1", e =>
        {
            e.Consumer<ConsumerMessage>();
            e.UseMessageRetry(r =>
            {
                r.Incremental(3, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
            });
        });
    }
}