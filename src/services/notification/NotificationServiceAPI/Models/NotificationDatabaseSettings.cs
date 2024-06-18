namespace NotificationServiceAPI.Models
{
    public class NotificationDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string NotificationCollectionName { get; set; } = null!;
    }
}
