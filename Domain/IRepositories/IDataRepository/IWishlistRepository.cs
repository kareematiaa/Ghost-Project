using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories.IDataRepository
{
    public interface IWishlistRepository
    {
        Task<Wishlist> GetWishlistByCustomerIdAsync(string customerId);
        Task CreateWishlistAsync(Wishlist wishlist);
    }
}
