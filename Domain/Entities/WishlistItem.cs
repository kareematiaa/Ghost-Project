using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
   public class WishlistItem 
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public Guid WishlistId { get; set; }
        public Wishlist Wishlist { get; set; } = null!;
    }
}
