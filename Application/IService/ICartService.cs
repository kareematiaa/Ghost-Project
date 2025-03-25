using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface ICartService
    {

        Task<List<CartItemDto>> GetCartItemsAsync(string customerId);
        Task AddToCartAsync(string customerId, Guid productVariantId, int quantity);
        Task ChangeItemQuantityAsync(string customerId, Guid productVariantId, int quantity);
        Task RemoveItemFromCartAsync(string customerId, Guid cartItemId);
        Task EmptyCartAsync(string customerId);
    }
}
