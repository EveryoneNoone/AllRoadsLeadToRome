using AllRoadsLeadToRome.Service.Order.Application.Repositories.Interfaces;
using AllRoadsLeadToRome.Service.Order.Infrastructure.Repositories.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace AllRoadsLeadToRome.Service.Order.Infrastructure;

public static class InfrastructureConfigureServices
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IOrderRepository, OrderRepository>();
        return services;
    }
}