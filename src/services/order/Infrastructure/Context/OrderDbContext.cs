using AllRoadsLeadToRome.Service.Order.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AllRoadsLeadToRome.Service.Order.Infrastructure.Context;

public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options)
        : base(options)
    { }

    public DbSet<AddressEntity> Addresses { get; set; } = null!;
    public DbSet<OrderEntity> Orders { get; set; } = null!;
    public DbSet<OrderLogEntity> OrderLogs { get; set; } = null!;
}