using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AllRoadsLeadToRome.Service.Order.Domain.Entities
{
    public class OrderEntity
    {
        [Key] public int Id { get; set; }
        [ForeignKey("Address")] public int AddressFromId { get; set; }
        public virtual AddressEntity AddressFrom { get; set; } = null!;
        [ForeignKey("Address")] public int AddressToId { get; set; }
        public virtual AddressEntity AddressTo { get; set; } = null!;
        public int CustomerUserId { get; set; }
        public int DeliveryUserId { get; set; }
        public decimal Weight { get; set; }
        public decimal DeliveryCost { get; set; }
        public virtual ICollection<OrderLogEntity> OrderLogs { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CompletedDate { get; set; }
    }
}