using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class WishlistItemDto
    {
       // public Guid WishlistId { get; set; }
        public Guid  VariantId { get; set; }
        public Guid  ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
