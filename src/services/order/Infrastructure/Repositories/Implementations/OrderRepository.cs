using AllRoadsLeadToRome.Service.Order.Application.Dtos;
using AllRoadsLeadToRome.Service.Order.Application.Repositories.Interfaces;
using AllRoadsLeadToRome.Service.Order.Domain.Entities;
using AllRoadsLeadToRome.Service.Order.Infrastructure.Context;

namespace AllRoadsLeadToRome.Service.Order.Infrastructure.Repositories.Implementations;

public class OrderRepository : IOrderRepository
{
    private readonly OrderDbContext _dbContext;

    public OrderRepository(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Create(AddOrderRequestDto request, CancellationToken ct)
    {
        var addressFrom = await _dbContext.Addresses.AddAsync(new AddressEntity
        {
            City = request.AddressFrom.City,
            House = request.AddressFrom.House,
            Street = request.AddressFrom.Street,
            PostalCode = request.AddressFrom.PostalCode
        }, ct);
        
        var addressTo = await _dbContext.Addresses.AddAsync(new AddressEntity
        {
            City = request.AddressTo.City,
            House = request.AddressTo.House,
            Street = request.AddressTo.Street,
            PostalCode = request.AddressTo.PostalCode
        }, ct);
        await _dbContext.SaveChangesAsync(ct);
        
        var order = await _dbContext.Orders.AddAsync(new OrderEntity
        {
            AddressFromId = addressFrom.Entity.Id,
            AddressToId = addressTo.Entity.Id,
            CustomerUserId = request.CustomerUserId,
            DeliveryUserId = request.DeliveryUserId,
            Weight = request.Weight,
            DeliveryCost = 0,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        }, ct);
        await _dbContext.SaveChangesAsync(ct);
        return order.Entity.Id;
    }
}
