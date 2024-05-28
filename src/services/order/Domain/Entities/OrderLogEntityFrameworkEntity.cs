using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AllRoadsLeadToRome.Core.Db;
using AllRoadsLeadToRome.Core.Enums;

namespace AllRoadsLeadToRome.Service.Order.Domain.Entities
{
    public class OrderLogEntityFrameworkEntity : BaseEntityFrameworkEntity
    {
        [ForeignKey("Order")] public int OrderId { get; set; } 
        public virtual OrderEntityFrameworkEntity Order { get; set; } = null!;
        public OrderStatus OrderStatus { get; set; }
    }
}