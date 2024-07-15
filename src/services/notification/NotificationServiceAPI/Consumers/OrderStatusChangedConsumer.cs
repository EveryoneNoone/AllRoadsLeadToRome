using AllRoadsLeadToRome.Core.MassTransit.Events;
using AllRoadsLeadToRome.Core.MassTransit.Messages;
using MassTransit;

namespace NotificationServiceAPI.Consumers;

public class OrderStatusChangedConsumer : IConsumer<MessageDto>
{
    private readonly ILogger<OrderStatusChangedConsumer> _logger;

    public OrderStatusChangedConsumer(ILogger<OrderStatusChangedConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<MessageDto> context)
    {
        //_logger.LogInformation(
        //    " [*] Message received Order id: {id} ,Order Status: {newOlderStatus} {customUserId} {deliveryUserId} {oldOrderStatus}",
        //    context.Message.Id,
        //    context.Message.NewOrderStatus,
        //    context.Message.CustomerUserId,
        //    context.Message.DeliveryUserId,
        //    context.Message.OldOrderStatus);
        return Task.CompletedTask;
    }
}
