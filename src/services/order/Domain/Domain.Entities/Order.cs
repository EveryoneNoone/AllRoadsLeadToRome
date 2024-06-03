using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    internal class Order
    {
        public int Id { get; set; }
        public Address From { get; set; }
        public Address To { get; set; }
        public int UserId { get; set; }
        public decimal Height { get; set; }
        public decimal Price { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime DoneDate { get; set; }
    }
}
