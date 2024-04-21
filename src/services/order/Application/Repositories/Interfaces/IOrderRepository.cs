using AllRoadsLeadToRome.Core.Enums;
using AllRoadsLeadToRome.Service.Order.Application.Dtos;
using AllRoadsLeadToRome.Service.Order.Domain.Entities;

namespace AllRoadsLeadToRome.Service.Order.Application.Repositories.Interfaces;

public interface IOrderRepository
{ 
    Task<OrderEntity> Create(AddOrderRequestDto request, CancellationToken ct);
    Task<OrderEntity> GetById(int id, CancellationToken ct);
    Task<IEnumerable<OrderEntity>> GetAll(CancellationToken ct);
    Task ChangeStatus(int id, OrderStatus newStatus, CancellationToken ct);
}