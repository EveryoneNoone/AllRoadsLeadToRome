using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // Задайте путь к проекту, где находится файл appsettings.json (путь к проекту WebApi)
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../WebApi");
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseNpgsql(connectionString);

            return new AppDbContext(builder.Options);
        }
    }
}
