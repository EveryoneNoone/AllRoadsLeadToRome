using AllRoadsLeadToRome.Core.MassTransit.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrustructure
{
    public interface IReceiverInfo
    {
        public string? Id { get; set; }
        public string Receiver {  get; set; }
        public string Message { get; set; }
        public NotificationType NotificationType { get; set; }
        public DateTime SendDateTime { get; set; }
        public bool SendResult { get; set; }
    }
}
