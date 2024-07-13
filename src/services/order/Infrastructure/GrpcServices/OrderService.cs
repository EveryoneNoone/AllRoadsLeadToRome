using AllRoadsLeadToRome.Service.Order.Application.Repositories.Interfaces;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using OrderApi;

namespace AllRoadsLeadToRome.Service.Order.Infrastructure.GrpcServices;

public class OrderService : OrderGrpc.OrderGrpcBase
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public override async Task<GetOrderResponse> GetOrder(GetOrderRequest request, ServerCallContext context)
    {
        var order = await _orderRepository.GetById(request.Id, CancellationToken.None);
        return new GetOrderResponse
        {
            Id = order.Id,
            AddressFrom = order.AddressFrom,
            AddressTo = order.AddressTo,
            Status = (int)order.Status,
            CustomerUserId = order.CustomerUserId,
            DeliveryUserId = order.DeliveryUserId,
            Weight = (double)order.Weight,
            DeliveryCost = (double)order.DeliveryCost,
            CompletedDate = order.CompletedDate.ToUniversalTime().ToTimestamp()
        };
    }
}
