using AllRoadsLeadToRome.Service.Order.Application.Dtos;

namespace AllRoadsLeadToRome.Service.Order.Application.Repositories.Interfaces;

public interface IOrderRepository
{ 
    Task<int> Create(AddOrderRequestDto request, CancellationToken ct);
}