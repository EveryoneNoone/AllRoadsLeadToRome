using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public bool DriverApproved { get; set; }
        public UserType Type { get; set; }
        public NotificationType NotificationPreference { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
