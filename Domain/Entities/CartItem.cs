using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CartItem : BaseEntity
    {
        public virtual Guid ProductVariantId { get; set; }
        public virtual Guid CartId { get; set; }
        public int Quantity { get; set; } = 1;
        public virtual Cart Cart { get; set; } = null!;
        public virtual ProductVariant ProductVariant { get; set; } = null!;
    }
}
