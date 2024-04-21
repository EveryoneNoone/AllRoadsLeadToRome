using AllRoadsLeadToRome.Service.Order.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AllRoadsLeadToRome.Service.Order.Infrastructure.Context;

public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options)
        : base(options)
    { }
    
    // public OrderDbContext()
    // { }
    //
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=123456");
    // }
    public DbSet<OrderEntity> Orders { get; set; } = null!;
    public DbSet<OrderLogEntity> OrderLogs { get; set; } = null!;
}