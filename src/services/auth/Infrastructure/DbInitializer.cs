using Core.Entities;
using Infrastructure.Context;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure
{
    public static class DbInitializer
    {
        //public static void Initialize(AppDbContext context)
        //{
        //    InitializeDatabase(context);
        //}

        //public static void InitializeDatabase(AppDbContext context)
        //{
        //    //context.Database.EnsureDeleted();
        //    context.Database.EnsureCreated();
        //}

        public static async Task Initialize(AppDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            string[] roleNames = { "User", "Driver", "Administrator" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Optionally seed a default admin user
            var adminUser = new User
            {
                UserName = "admin@example.com",
                Email = "admin@example.com",
                FullName = "Admin User",
                Type = UserType.Administrator,
                NotificationPreference = NotificationType.Email
            };

            var adminPassword = "Admin@123";

            var user = await userManager.FindByEmailAsync(adminUser.Email);

            if (user == null)
            {
                var createAdminUser = await userManager.CreateAsync(adminUser, adminPassword);
                if (createAdminUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Administrator");
                }
            }
        }
    }
}
