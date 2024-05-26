using AllRoadsLeadToRome.Service.Order.Application.Dtos;
using AllRoadsLeadToRome.Service.Order.Domain.Entities;

namespace AllRoadsLeadToRome.Service.Order.Application.Repositories.Interfaces;

public interface IOrderLogRepository
{ 
    Task Create(OrderEntityFrameworkEntity order, CancellationToken ct);
}