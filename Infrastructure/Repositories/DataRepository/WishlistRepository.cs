using Domain.Entities;
using Domain.IRepositories.IDataRepository;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.DataRepository
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly GhostContext _context;

        public WishlistRepository(GhostContext context)
        {
            _context = context;
        }

        public async Task<Wishlist> GetWishlistByCustomerIdAsync(string customerId)
        {
            return await _context.Wishlists
                .Include(w => w.WishlistItems)
                    .ThenInclude(wi => wi.Product)
                        .ThenInclude(pv => pv.ProductVariants)
                        .ThenInclude(pc => pc.ProductImages)        
                .FirstOrDefaultAsync(w => w.CustomerId == customerId);
        }



        public async Task CreateWishlistAsync(Wishlist wishlist)
        {
            await _context.Wishlists.AddAsync(wishlist);
            await _context.SaveChangesAsync();
        }

    }
}
