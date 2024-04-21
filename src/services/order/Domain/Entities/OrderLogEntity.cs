using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AllRoadsLeadToRome.Core.Db;
using AllRoadsLeadToRome.Core.Enums;

namespace AllRoadsLeadToRome.Service.Order.Domain.Entities
{
    public class OrderLogEntity : BaseEntity
    {
        [ForeignKey("Order")] public int OrderId { get; set; } 
        public virtual OrderEntity Order { get; set; } = null!;
        public OrderStatus OrderStatus { get; set; }
    }
}