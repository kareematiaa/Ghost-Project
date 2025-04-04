using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductVariantSize :  BaseEntity
    {

        public Guid ProductVariantId { get; set; }
        public ProductVariant ProductVariant { get; set; } = null!;

        public Guid SizeId { get; set; }
        public ProductSize Size { get; set; } = null!;

        // You can add additional properties like:
        public int QuantityAvailable { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
