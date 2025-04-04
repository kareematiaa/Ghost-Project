using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderProductVariant : BaseEntity
    {

        public Guid OrderId { get; set; }
        public Guid ProductVariantId { get; set; }
        public int Quantity { get; set; }
        public Order Order { get; set; } = null!;
        public ProductVariant ProductVariant { get; set; } = null!;
        public Guid SizeId { get; set; }  // 👈 New field to store the selected size
        public ProductSize Size { get; set; } = null!;

    }
}
