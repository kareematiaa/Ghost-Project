using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public ICollection<ProductVariant> ProductVariants { get; set; } = null!;
        public ICollection<WishlistItem> WishListItems { get; set; } = null!;


    }
}
