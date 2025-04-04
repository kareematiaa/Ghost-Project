using Application.DTOs;
using Domain.Users;
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
        Task AddToCartAsync(string customerId, Guid productVariantId,Guid sizeId, int quantity);
        Task ChangeItemQuantityAsync(string customerId, Guid productVariantId,Guid sizeId ,int quantity);
        Task RemoveItemFromCartAsync(string customerId,Guid sizeId ,Guid cartItemId);
        Task EmptyCartAsync(string customerId);
        Task<ICustomer> GetUserByIdAsync(string userId);
    }
}
