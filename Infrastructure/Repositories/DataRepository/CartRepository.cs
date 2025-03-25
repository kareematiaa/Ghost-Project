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
    public class CartRepository :ICartRepository
    {
        private readonly GhostContext _context;

        public CartRepository(GhostContext context)
        {
            _context = context;
        }

        public async Task<CartItem?> GetCartItemAsync(Guid cartId, Guid productVariantId)
        {
            return await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.CartId == cartId && ci.ProductVariantId == productVariantId);
        }

        public async Task<Cart> GetCartByCustomerIdAsync(string customerId)
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.ProductVariant)
                        .ThenInclude(pv => pv.Product)
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.ProductVariant)
                        .ThenInclude(pv => pv.ProductColor)
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.ProductVariant)
                        .ThenInclude(pv => pv.ProductSize)
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.ProductVariant)
                        .ThenInclude(pv => pv.ProductImages)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);
        }

        public async Task AddToCartAsync(CartItem cartItem)
        {
            bool productExists = await _context.CartItems
           .AnyAsync(wi => wi.CartId == cartItem.CartId && wi.ProductVariantId == cartItem.ProductVariantId);

            if (productExists)
            {
                throw new AlreadyExistException("Product variant");
            }
            await _context.CartItems.AddAsync(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCartItemAsync(CartItem cartItem)
        {
            _context.CartItems.Update(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveFromCartAsync(Guid cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task EmptyCartAsync(Guid cartId)
        {
            var cartItems = await _context.CartItems
                .Where(ci => ci.CartId == cartId)
                .ToListAsync();

            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
        }
    }
}
