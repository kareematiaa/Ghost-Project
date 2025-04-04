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
        Task UpdateCartItemQuantityAsync(string customerId, Guid productVariantId, Guid sizeId, int quantity);
        Task RemoveCartItemAsync(string customerId, Guid productVariantId, Guid sizeId);
        Task EmptyCartAsync(string customerId);
        Task CreateCartForCustomerAsync(string customerId);
    }
}
