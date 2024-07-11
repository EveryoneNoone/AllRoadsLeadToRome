using AllRoadsLeadToRome.Core.MassTransit.Enums;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrustructure
{
    public class ReceiverInfo : IReceiverInfo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Receiver")]
        public string Receiver { get; set; }

        [BsonElement("Message")]
        public string Message { get; set; }

        [BsonElement("NotificationType")]
        public NotificationType NotificationType { get; set; }
        [BsonElement("SendDateTime")]
        public DateTime SendDateTime { get; set; }
        [BsonElement("SendResult")]
        public bool SendResult { get; set; }
    }
}
