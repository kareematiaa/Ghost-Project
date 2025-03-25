using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class WishlistDto
    {
        public Guid CustomerId { get; set; }
        public List<WishlistItemDto> wishlistItems { get; set; }
    }
}
