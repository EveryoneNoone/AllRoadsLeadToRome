using AllRoadsLeadToRome.Core.Enums;
using AllRoadsLeadToRome.Service.Order.Application.Dtos;
using AllRoadsLeadToRome.Service.Order.Domain.Entities;

namespace AllRoadsLeadToRome.Service.Order.Application.Repositories.Interfaces;

public interface IOrderRepository
{ 
    Task<OrderEntityFrameworkEntity> Create(AddOrderRequestDto request, CancellationToken ct);
    Task<OrderEntityFrameworkEntity> GetById(int id, CancellationToken ct);
    Task<IEnumerable<OrderEntityFrameworkEntity>> GetAll(CancellationToken ct);
    Task ChangeStatus(int id, OrderStatus newStatus, CancellationToken ct);
}