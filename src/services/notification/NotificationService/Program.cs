using MassTransit;
using Infrustructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NotificationService;
using CommonNamespace;
using Application.Consumers;

namespace NotificationService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            return Host.CreateDefaultBuilder(args)
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
}