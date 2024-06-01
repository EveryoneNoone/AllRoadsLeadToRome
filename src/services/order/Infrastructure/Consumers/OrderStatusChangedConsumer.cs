using AllRoadsLeadToRome.Core.MassTransit.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace AllRoadsLeadToRome.Service.Order.Infrastructure.Consumers;

public class OrderStatusChangedConsumer : IConsumer<OrderStatusChangedEvent>
{
    private readonly ILogger<OrderStatusChangedConsumer> _logger;

    public OrderStatusChangedConsumer(ILogger<OrderStatusChangedConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<OrderStatusChangedEvent> context)
    {
        _logger.LogInformation(" [*] Message received Order id: {code} ,Order Status: {name} ",context.Message.Id,context.Message.NewOrderStatus);
        return Task.CompletedTask;
    }
}