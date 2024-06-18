using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllRoadsLeadToRome.Core.MassTransit.Enums;

namespace Infrustructure
{
    public class Template
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        public string? Name { get; set; }

        [BsonElement("Value")]
        public string? Value { get; set; }

        [BsonElement("TemplateType")]
        public NotificationType TemplateType { get; set; }
    }
}
