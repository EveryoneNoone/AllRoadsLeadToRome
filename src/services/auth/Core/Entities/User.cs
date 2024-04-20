namespace Core.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool DriverApproved { get; set; }
        public UserType Type { get; set; }
        public NotificationType NotificationPreference { get; set; }
    }
}
