using System.ComponentModel.DataAnnotations;

namespace AllRoadsLeadToRome.Service.Order.Domain.Entities
{
    public class AddressEntity
    {
        [Key] public int Id { get; set; }
        public string City { get; set; }
        public int Region { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
    }
}