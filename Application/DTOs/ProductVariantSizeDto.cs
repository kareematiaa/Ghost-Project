using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ProductVariantSizeDto
    {
        public Guid SizeId { get; set; }
        public int QuantityAvailable { get; set; }
        public bool IsActive { get; set; }
    }
}
