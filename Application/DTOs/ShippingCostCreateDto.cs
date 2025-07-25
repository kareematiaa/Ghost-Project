using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ShippingCostCreateDto
    {
        public string Governate { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
