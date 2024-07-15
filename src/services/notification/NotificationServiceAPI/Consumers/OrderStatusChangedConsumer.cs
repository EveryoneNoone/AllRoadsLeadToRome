using AllRoadsLeadToRome.Core.MassTransit.Events;
using MassTransit;

namespace NotificationServiceAPI.Consumers;

public class OrderStatusChangedConsumer : IConsumer<OrderStatusChangedEvent>
{
    private readonly ILogger<OrderStatusChangedConsumer> _logger;

    public OrderStatusChangedConsumer(ILogger<OrderStatusChangedConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<OrderStatusChangedEvent> context)
    {
        _logger.LogInformation(
            " [*] Message received Order id: {id} ,Order Status: {newOlderStatus} {customUserId} {deliveryUserId} {oldOrderStatus}",
            context.Message.Id,
            context.Message.NewOrderStatus,
            context.Message.CustomerUserId,
            context.Message.DeliveryUserId,
            context.Message.OldOrderStatus);
        return Task.CompletedTask;
    }
}
