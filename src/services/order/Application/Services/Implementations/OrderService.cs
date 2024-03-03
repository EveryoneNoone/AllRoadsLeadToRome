using AllRoadsLeadToRome.Service.Order.Application.Dtos;
using AllRoadsLeadToRome.Service.Order.Application.Repositories.Interfaces;
using AllRoadsLeadToRome.Service.Order.Application.Services.Interfaces;

namespace AllRoadsLeadToRome.Service.Order.Application.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<int> Create(AddOrderRequestDto request, CancellationToken ct)
    {
        return await _orderRepository.Create(request, ct);
    }
}