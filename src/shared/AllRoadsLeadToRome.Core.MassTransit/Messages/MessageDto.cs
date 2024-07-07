using AllRoadsLeadToRome.Core.MassTransit.Enums;
namespace AllRoadsLeadToRome.Core.MassTransit.Messages
{    
    public class MessageDto
    {
        public Dictionary<string, string> Content { get; set; }
        public string Receiver { get; set; }
        //public string Data { get; set; }
        public NotificationType TypeNotification { get; set; }
        public string TemplateName { get; set; }
    }
}
