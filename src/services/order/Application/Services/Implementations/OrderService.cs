using Application.Repositories.Interfaces;
using Application.Services.Interfaces;

namespace Application.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task Create(CancellationToken ct)
    {
        await _orderRepository.Create(ct);
    }
}