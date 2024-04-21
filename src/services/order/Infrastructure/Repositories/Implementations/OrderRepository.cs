using AllRoadsLeadToRome.Core.Db;
using AllRoadsLeadToRome.Core.Enums;
using AllRoadsLeadToRome.Service.Order.Application.Dtos;
using AllRoadsLeadToRome.Service.Order.Application.Repositories.Interfaces;
using AllRoadsLeadToRome.Service.Order.Domain.Entities;
using AllRoadsLeadToRome.Service.Order.Infrastructure.Context;

namespace AllRoadsLeadToRome.Service.Order.Infrastructure.Repositories.Implementations;

public class OrderRepository : EntityFrameworkRepository<OrderEntity>, IOrderRepository
{
    private readonly OrderDbContext _dbContext;

    public OrderRepository(OrderDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OrderEntity> Create(AddOrderRequestDto request, CancellationToken ct)
    {
        var order = await Add(new OrderEntity
        {
            AddressFrom = $"{request.AddressFrom.PostalCode} {request.AddressFrom.City} {request.AddressFrom.Street}",
            AddressTo = $"{request.AddressTo.PostalCode} {request.AddressTo.City} {request.AddressTo.Street}",
            CustomerUserId = request.CustomerUserId,
            DeliveryUserId = request.DeliveryUserId,
            Weight = request.Weight,
            Status = OrderStatus.Created,
            DeliveryCost = 0,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        }, ct);
       
        return order;
    }

    public async Task ChangeStatus(int id, OrderStatus newStatus, CancellationToken ct)
    {
        await Update(id, entity =>
        {
            entity.UpdatedDate = DateTime.UtcNow;
            entity.Status = newStatus;
        }, ct);
    }
}
