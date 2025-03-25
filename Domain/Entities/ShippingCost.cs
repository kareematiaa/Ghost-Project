using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ShippingCost 
    {
        public Guid ShippingMethodId { get; set; }
        public ShippingMethod ShippingMethod { get; set; } = null!;
        public string Governate { get; set; } = null!;
        public decimal Price { get; set; }
        public ICollection<Order> Orders { get; set; } = null!;
    }
}
