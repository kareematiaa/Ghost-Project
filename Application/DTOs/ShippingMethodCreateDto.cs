using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ShippingMethodCreateDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string EstimatedDelivery { get; set; } = null!;
    }
}
