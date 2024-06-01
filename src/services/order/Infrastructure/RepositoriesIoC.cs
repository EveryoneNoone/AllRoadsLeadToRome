using System.Reflection;
using AllRoadsLeadToRome.Service.Order.Application.Repositories.Interfaces;
using AllRoadsLeadToRome.Service.Order.Infrastructure.Context;
using AllRoadsLeadToRome.Service.Order.Infrastructure.Repositories.Implementations;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AllRoadsLeadToRome.Service.Order.Infrastructure;

public static class InfrastructureConfigureServices
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        RegisterMassTransit(services);
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderLogRepository, OrderLogRepository>();
        return services;
    }
    
    private static void RegisterMassTransit(IServiceCollection services)
    {
        services.AddMassTransit(m=>
        {
            m.AddConsumers(Assembly.GetExecutingAssembly());
            m.UsingRabbitMq((ctx,cfg)=>
            {
                cfg.Host("localhost","/",c=>
                {
                    c.Username("guest");
                    c.Password("guest");
                });
                cfg.ConfigureEndpoints(ctx);
            });
        });
    }
}