using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories.IDataRepository
{
    public interface ICartRepository
    {
        Task<CartItem?> GetCartItemAsync(Guid cartId, Guid productVariantId);
        Task<Cart> GetCartByCustomerIdAsync(string customerId);
        Task AddToCartAsync(CartItem cartItem);
        Task UpdateCartItemAsync(CartItem cartItem);
        Task RemoveFromCartAsync(Guid cartItemId);
        Task EmptyCartAsync(Guid cartId);
    }
}
