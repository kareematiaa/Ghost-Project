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

        public async Task<Cart?> GetCartByCustomerIdAsync(string customerId)
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Size) // 🔹 Ensure Size is included
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.ProductVariant)
                        .ThenInclude(pv => pv.Product)
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.ProductVariant)
                        .ThenInclude(pv => pv.ProductColor)
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.ProductVariant)
                        .ThenInclude(pv => pv.ProductImages)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);
        }

        public async Task AddToCartAsync(CartItem cartItem)
        {
            bool productExists = await _context.CartItems
           .AnyAsync(wi => wi.CartId == cartItem.CartId && wi.ProductVariantId == cartItem.ProductVariantId && wi.SizeId == cartItem.SizeId);

            if (productExists)
            {
                throw new AlreadyExistException("Product");
            }
            await _context.CartItems.AddAsync(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCartItemQuantityAsync(string customerId, Guid productVariantId, Guid sizeId, int quantity)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (cart == null) throw new NotFoundException("Cart ");

            var item = cart.CartItems.FirstOrDefault(ci =>
                ci.ProductVariantId == productVariantId && ci.SizeId == sizeId);

            if (item == null) throw new NotFoundException("Cart item ");

            item.Quantity = quantity;
            await _context.SaveChangesAsync();
        }

        public async Task RemoveCartItemAsync(string customerId, Guid productVariantId, Guid sizeId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (cart == null) return;

            var item = cart.CartItems.FirstOrDefault(ci =>
                ci.ProductVariantId == productVariantId && ci.SizeId == sizeId);

            if (item != null)
            {
                _context.CartItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task EmptyCartAsync(string customerId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (cart == null) return;

            _context.CartItems.RemoveRange(cart.CartItems);
            await _context.SaveChangesAsync();
        }

        public async Task CreateCartForCustomerAsync(string customerId)
        {
            var cart = new Cart { CustomerId = customerId };
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
        }
    }
}
