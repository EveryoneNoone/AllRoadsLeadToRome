using AllRoadsLeadToRome.Core.Db;
using AllRoadsLeadToRome.Service.Order.Application.Repositories.Interfaces;
using AllRoadsLeadToRome.Service.Order.Domain.Entities;
using AllRoadsLeadToRome.Service.Order.Infrastructure.Context;

namespace AllRoadsLeadToRome.Service.Order.Infrastructure.Repositories.Implementations;

public class OrderLogRepository : EntityFrameworkRepository<OrderLogEntityFrameworkEntity>, IOrderLogRepository
{
    private readonly OrderDbContext _dbContext;

    public OrderLogRepository(OrderDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task Create(OrderEntityFrameworkEntity order, CancellationToken ct)
    {
        await Add(new OrderLogEntityFrameworkEntity
        {
            OrderId = order.Id,
            OrderStatus = order.Status,
            CreatedDate = DateTime.UtcNow,
        }, ct);
    }
}
