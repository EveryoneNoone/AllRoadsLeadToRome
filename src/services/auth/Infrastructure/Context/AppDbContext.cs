using Core.Entities;
using Infrastructure.Configurations;
using Infrastructure.Data;
using Infrastructure.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply entity configurations
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserSessionConfiguration());

            SeedFakeData.Initialize(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}
