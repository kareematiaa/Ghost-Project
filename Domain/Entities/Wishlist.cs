using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Wishlist : BaseEntity
    {
        public string CustomerId { get; set; } = null!;
        public ICustomer Customer { get; set; } = null!;
        public ICollection<WishlistItem>? WishlistItems { get; set; }
    }
}
