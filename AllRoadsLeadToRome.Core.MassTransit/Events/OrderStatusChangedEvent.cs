using AllRoadsLeadToRome.Core.Enums;

namespace AllRoadsLeadToRome.Core.MassTransit.Events;

public class OrderStatusChangedEvent
{
    public int Id { get; set; }
    public OrderStatus OldOrderStatus { get; set; }
    public OrderStatus NewOrderStatus { get; set; }
    public int CustomerUserId { get; set; }
    public int DeliveryUserId { get; set; }
     
}