using AllRoadsLeadToRome.Core.Db;
using AllRoadsLeadToRome.Core.Enums;
using AllRoadsLeadToRome.Core.MassTransit.Events;
using AllRoadsLeadToRome.Service.Order.Application.Dtos;
using AllRoadsLeadToRome.Service.Order.Application.Repositories.Interfaces;
using AllRoadsLeadToRome.Service.Order.Domain.Entities;
using AllRoadsLeadToRome.Service.Order.Infrastructure.Context;
using MassTransit;

namespace AllRoadsLeadToRome.Service.Order.Infrastructure.Repositories.Implementations;

public class OrderRepository : EntityFrameworkRepository<OrderEntityFrameworkEntity>, IOrderRepository
{
    private readonly OrderDbContext _dbContext;
    private readonly IBus _bus;

    public OrderRepository(OrderDbContext dbContext, IBus bus) : base(dbContext)
    {
        _dbContext = dbContext;
        _bus = bus;
    }

    public async Task<OrderEntityFrameworkEntity> Create(AddOrderRequestDto request, CancellationToken ct)
    {
        var order = await Add(new OrderEntityFrameworkEntity
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
        await _bus.Publish<OrderStatusChangedEvent>(new 
        {
            Id = id,
            OrderStatus = newStatus
        }, ct);
    }
}
