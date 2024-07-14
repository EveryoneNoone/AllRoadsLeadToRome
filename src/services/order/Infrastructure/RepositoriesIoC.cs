using AllRoadsLeadToRome.Service.Order.Application.Repositories.Interfaces;
using AllRoadsLeadToRome.Service.Order.Infrastructure.Repositories.Implementations;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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
            m.UsingRabbitMq((ctx,cfg)=>
            {
                cfg.Host(rabbitMqSettings["Host"], "/", c =>
                {
                    c.Username(rabbitMqSettings["Username"]);
                    c.Password(rabbitMqSettings["Password"]);
                });
                cfg.ConfigureEndpoints(ctx);
            });
        });
    }
}