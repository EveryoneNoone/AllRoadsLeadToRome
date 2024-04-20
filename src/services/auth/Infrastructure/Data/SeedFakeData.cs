using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public static class SeedFakeData
    {
        public static void Initialize(ModelBuilder modelBuilder)
        {
            var userId = Guid.NewGuid(); // Сохраняем этот GUID для использования в связанных сущностях

            // Seed data for User
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = userId,
                    FullName = "John Doe",
                    Email = "johndoe@example.com",
                    Phone = "1234567890",
                    Password = "12345",
                    DriverApproved = true,
                    Type = UserType.User,
                    NotificationPreference = NotificationType.Email
                }
            );

            // Seed data for UserSession
            modelBuilder.Entity<UserSession>().HasData(
                new UserSession
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    AccessToken = Guid.NewGuid(),
                    RefreshToken = Guid.NewGuid(),
                    StartDate = DateTime.UtcNow,
                    FinishDate = null,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    DeletedAt = DateTime.MinValue
                }
            );
        }
    }
}
