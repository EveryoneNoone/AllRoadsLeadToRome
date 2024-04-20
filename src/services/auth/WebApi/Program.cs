
using Infrastructure;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Add services to the container.
            builder.Services.AddScoped(typeof(IRepository<>), typeof(EfCoreRepository<>));
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
                options.EnableSensitiveDataLogging();
            });

            builder.Services.AddControllers();

            // Configure Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            //app.UseAuthorization();
            app.MapControllers();

            using var scope = app.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            try
            {
                var context = serviceProvider.GetRequiredService<AppDbContext>();
                DbInitializer.Initialize(context);
            }
            catch (Exception ex)
            {
                // TODO: Заменить в будущем на Logger
                Console.WriteLine(ex);
            }

            app.Run();
        }
    }
}
