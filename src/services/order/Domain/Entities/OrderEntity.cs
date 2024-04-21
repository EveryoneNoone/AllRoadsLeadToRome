using AllRoadsLeadToRome.Core.Db;
using AllRoadsLeadToRome.Core.Enums;

namespace AllRoadsLeadToRome.Service.Order.Domain.Entities
{
    public class OrderEntity : BaseEntity
    {
        public string AddressFrom { get; set; }
        public string AddressTo { get; set; }
        public OrderStatus Status { get; set; }
        public int CustomerUserId { get; set; }
        public int DeliveryUserId { get; set; }
        public decimal Weight { get; set; }
        public decimal DeliveryCost { get; set; }
        public virtual ICollection<OrderLogEntity> OrderLogs { get; set; }
        public DateTime CompletedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}