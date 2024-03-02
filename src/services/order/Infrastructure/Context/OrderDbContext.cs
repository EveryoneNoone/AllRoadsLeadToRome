using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class OrderDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=123456");
    }

    public OrderDbContext() : base(new DbContextOptions<OrderDbContext>())
    {
    }

    public DbSet<AddressEntity> Addresses { get; set; } = null!;
    public DbSet<OrderEntity> Orders { get; set; } = null!;
    public DbSet<OrderLogEntity> OrderLogs { get; set; } = null!;
}