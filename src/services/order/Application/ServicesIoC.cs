using AllRoadsLeadToRome.Service.Order.Application.Services.Implementations;
using AllRoadsLeadToRome.Service.Order.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AllRoadsLeadToRome.Service.Order.Application;

public static class ServicesIoC
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IOrderService, OrderService>();

        return services;
    }
}