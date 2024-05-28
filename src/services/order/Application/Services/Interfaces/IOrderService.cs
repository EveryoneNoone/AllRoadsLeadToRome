using AllRoadsLeadToRome.Service.Order.Application.Dtos;

namespace AllRoadsLeadToRome.Service.Order.Application.Services.Interfaces;

public interface IOrderService
{
    Task<int> Create(AddOrderRequestDto request, CancellationToken ct);
}