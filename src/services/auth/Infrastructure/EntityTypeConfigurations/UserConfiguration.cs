using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(255);
            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(u => u.Phone).IsRequired().HasMaxLength(20);
            builder.HasIndex(u => u.Phone).IsUnique();
            builder.Property(u => u.Password).IsRequired().HasMaxLength(255);
            builder.Property(u => u.FullName).IsRequired().HasMaxLength(255);
            builder.Property(u => u.DriverApproved).IsRequired();

            builder.Property(u => u.Type)
                   .HasConversion(
                       v => v.ToString(),
                       v => (UserType)Enum.Parse(typeof(UserType), v));
            builder.Property(u => u.NotificationPreference)
                   .HasConversion(
                       v => v.ToString(),
                       v => (NotificationType)Enum.Parse(typeof(NotificationType), v));
        }
    }
}
