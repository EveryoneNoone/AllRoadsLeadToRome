using AllRoadsLeadToRome.Core.MassTransit.Events;
using AllRoadsLeadToRome.Core.MassTransit.Messages;
using AllRoadsLeadToRome.Service.Order.Application.Repositories.Interfaces;
using AllRoadsLeadToRome.Service.Order.Infrastructure.Repositories.Implementations;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AllRoadsLeadToRome.Service.Order.Infrastructure;

public static class InfrastructureConfigureServices
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        RegisterMassTransit(services, configuration);
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderLogRepository, OrderLogRepository>();
        return services;
    }

    private static void RegisterMassTransit(IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMqSettings = configuration.GetSection("RabbitMQ");

        services.AddMassTransit(m =>
        {
            //m.AddConsumers(Assembly.GetExecutingAssembly());
            m.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(rabbitMqSettings["Host"], "/", c =>
                {
                    c.Username(rabbitMqSettings["Username"]);
                    c.Password(rabbitMqSettings["Password"]);
                });
                //cfg.ConfigureEndpoints(ctx);
                cfg.Message<MessageDto>(x =>
                {
                    x.SetEntityName("masstransit_event_queue_1"); // Устанавливаем имя очереди
                });
            });
        });
    }
}