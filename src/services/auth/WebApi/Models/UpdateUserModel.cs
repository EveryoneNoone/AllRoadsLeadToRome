using Core.Entities;

namespace WebApi.Models;

public class UpdateUserModel
{
    public string Id { get; set; }
    public string FullName { get; set; }
    public bool DriverApproved { get; set; }
    public UserType Type { get; set; }
    public NotificationType NotificationPreference { get; set; }
}
