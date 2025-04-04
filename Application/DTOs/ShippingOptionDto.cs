using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ShippingOptionDto
    {
        public Guid ShippingMethodId { get; set; }
        public string Name { get; set; } = null!;
        public string EstimatedDelivery { get; set; } = null!;
        public string Governate { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
