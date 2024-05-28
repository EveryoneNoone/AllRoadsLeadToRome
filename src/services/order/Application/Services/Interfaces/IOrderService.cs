using AllRoadsLeadToRome.Core.Enums;
using AllRoadsLeadToRome.Service.Order.Application.Dtos;

namespace AllRoadsLeadToRome.Service.Order.Application.Services.Interfaces;

public interface IOrderService
{
    Task<int> Create(AddOrderRequestDto request, CancellationToken ct);
    Task<OrderResponseDto> GetById(int id, CancellationToken ct);
    Task<List<OrderResponseDto>> GetAll(CancellationToken ct);
    Task ChangeStatus(int id, OrderStatus newStatus, CancellationToken ct);
}