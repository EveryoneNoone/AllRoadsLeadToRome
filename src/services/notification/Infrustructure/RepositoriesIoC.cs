using System.Reflection;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Infrustructure;

public static class InfrastructureConfigureServices
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        RegisterMassTransit(services);
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