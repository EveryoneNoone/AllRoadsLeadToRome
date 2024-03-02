using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
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