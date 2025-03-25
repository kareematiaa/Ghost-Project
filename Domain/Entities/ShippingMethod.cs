using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ShippingMethod : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; } = null!;
        public string EstimatedDelivery { get; set; } = null!;
        public ICollection<ShippingCost> ShippingCosts { get; set; } = null!;
    }
}
