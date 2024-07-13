using Core.Entities;

namespace WebApi.Models;

public class RegisterModel
{
    public string FullName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public bool DriverApproved { get; set; }
    public required UserType Type { get; set; }
    public NotificationType NotificationPreference { get; set; }
}
