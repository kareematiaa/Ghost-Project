using Domain.Entities;
using Domain.Exceptions;
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
    public class WishlistitemsRepository : IWishlistItemsRepository
    {
        private readonly GhostContext _context;

        public WishlistitemsRepository(GhostContext context)
        {
            _context = context;
        }

        public async Task AddToWishlistAsync(WishlistItem wishlistItem)
        {
            bool productExists = await _context.WishlistItems
              .AnyAsync(wi => wi.WishlistId == wishlistItem.WishlistId && wi.ProductId == wishlistItem.ProductId);

            if (productExists)
            {
                throw new AlreadyExistException("Product");
            }

            await _context.WishlistItems.AddAsync(wishlistItem);
            await _context.SaveChangesAsync();
        }

     

        public async Task<List<WishlistItem>> GetWishlistItemsAsync(Guid wishlistId)
        {
            return await _context.WishlistItems
                .Include(wi => wi.Product)
                    .ThenInclude(pv => pv.ProductVariants)
                    .ThenInclude(pc => pc.ProductImages)
            
                .Where(wi => wi.WishlistId == wishlistId)
                .ToListAsync();
        }


        public async Task RemoveFromWishlistAsync(Guid wishlistId, Guid productId)
        {
            var wishlistItem = await _context.WishlistItems
                .FirstOrDefaultAsync(wi => wi.WishlistId == wishlistId && wi.ProductId == productId);

            if (wishlistItem != null)
            {
                _context.WishlistItems.Remove(wishlistItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
