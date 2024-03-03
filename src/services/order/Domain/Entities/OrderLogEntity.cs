using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AllRoadsLeadToRome.Core.Enums;

namespace AllRoadsLeadToRome.Service.Order.Domain.Entities
{
    public class OrderLogEntity
    {
        [Key] public string Id { get; set; } = null!;
        [ForeignKey("Order")] public int OrderId { get; set; } 
        public virtual OrderEntity Order { get; set; } = null!;
        public OrderStatus OrderStatus { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}