using Core.Entities;

namespace WebApi.Models;

public class RegisterModel
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool DriverApproved { get; set; }
    public UserType Type { get; set; }
    public NotificationType NotificationPreference { get; set; }
}


