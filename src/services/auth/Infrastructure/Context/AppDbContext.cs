using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var type in Enum.GetNames(typeof(UserType)))
        {
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = type, NormalizedName = type.ToUpper() });
        }

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.FullName).IsRequired();
            entity.Property(e => e.DriverApproved).HasColumnType("boolean").IsRequired();
            entity.Property(e => e.Type).IsRequired();
            entity.Property(e => e.NotificationPreference).IsRequired();
            entity.Property(e => e.RefreshToken).IsRequired();
            entity.Property(e => e.RefreshTokenExpiryTime).IsRequired();
        });
    }
}
