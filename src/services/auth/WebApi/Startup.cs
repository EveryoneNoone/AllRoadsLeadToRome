using AllRoadsLeadToRome.Core.Auth;
using Core.Entities;
using Infrastructure;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
#if DEBUG
                options.UseSqlite(Configuration.GetConnectionString("DefaultSQLiteConnection"));
#else
                options.UseNpgsql(Configuration.GetConnectionString("DefaultPostgresConnection"));
#endif
                options.EnableSensitiveDataLogging();
            });

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = false;
                options.SignIn.RequireConfirmedEmail = false;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            // JWT configuration
            services.AddCustomAuthentication(Configuration);

            services.AddScoped(typeof(IRepository<>), typeof(EfCoreRepository<>));
            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddCustomSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            // context.Database.Migrate();

            //if (env.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            try
            {
                var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                DbInitializer.Initialize(context, userManager, roleManager).Wait();
            }
            catch (Exception ex)
            {
                // TODO: Заменить в будущем на Logger
                Console.WriteLine(ex);
            }
        }
    }
}
