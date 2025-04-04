using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductVariant : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
        public Guid ColorId { get; set; } 
        public ProductColor ProductColor { get; set; } = null!;
        // Size relationships (many sizes available for this variant)
        public ICollection<ProductVariantSize> AvailableSizes { get; set; } = new List<ProductVariantSize>();
        public bool IsDeleted { get; set; }
        public ICollection<CartItem> CartItems { get; set; } = null!;
        public ICollection<ProductImage> ProductImages { get; set; } = null!;
        public ICollection<OrderProductVariant> OrderProductVariants { get; set; } = null!;

    }
}
