using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories.IDataRepository
{
    public interface IWishlistItemsRepository
    {
        Task AddToWishlistAsync(WishlistItem wishlistItem);
        Task RemoveFromWishlistAsync(Guid wishlistId, Guid productId); // New method
        Task<List<WishlistItem>> GetWishlistItemsAsync(Guid wishlistId);
    }
}
