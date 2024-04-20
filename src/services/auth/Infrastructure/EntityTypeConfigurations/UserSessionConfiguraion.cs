using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityTypeConfigurations
{
    public class UserSessionConfiguration : IEntityTypeConfiguration<UserSession>
    {
        public void Configure(EntityTypeBuilder<UserSession> builder)
        {
            builder.HasKey(us => us.Id);
            builder.Property(us => us.AccessToken).IsRequired().HasMaxLength(36); // Assuming GUID stored as string
            builder.Property(us => us.RefreshToken).IsRequired().HasMaxLength(36);
            builder.Property(us => us.StartDate).IsRequired();
            builder.Property(us => us.FinishDate);

            builder.HasOne(us => us.User)
                   .WithMany()
                   .HasForeignKey(us => us.UserId)
                   .IsRequired();
        }
    }
}
