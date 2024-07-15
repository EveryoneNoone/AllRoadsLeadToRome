using AllRoadsLeadToRome.Service.Order.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AllRoadsLeadToRome.Service.Order.Infrastructure.Context;

public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options)
        : base(options)
    { }

    public OrderDbContext()
    { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //        optionsBuilder.UseNpgsql("Host=localhost;Port=5434;Database=orderdb;Username=postgres;Password=123456");
        ////#if DEBUG
        ////        optionsBuilder.UseNpgsql("Host=localhost;Port=5434;Database=orderdb;Username=postgres;Password=123456");
        ////#else
        optionsBuilder.UseNpgsql("Host=orderservice_db;Port=5432;Database=orderdb;Username=postgres;Password=123456");
        //#endif
    }
    public DbSet<OrderEntityFrameworkEntity> Orders { get; set; } = null!;
    public DbSet<OrderLogEntityFrameworkEntity> OrderLogs { get; set; } = null!;
}