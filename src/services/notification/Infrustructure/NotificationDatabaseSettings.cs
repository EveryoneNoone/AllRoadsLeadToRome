namespace Infrustructure
{
    public class NotificationDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string NotificationCollectionName { get; set; } = null!;
        public string TemplateCollectionName { get; set; } = null!;
    }
}
