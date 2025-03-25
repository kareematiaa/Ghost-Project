using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IWishlistItemsService
    {
        Task AddToWishlistAsync(string customerId, Guid productId);
        Task RemoveFromWishlistAsync(string customerId, Guid productId);
        Task<List<WishlistItemDto>> GetWishlistItemsAsync(string customerId);
    }
}
