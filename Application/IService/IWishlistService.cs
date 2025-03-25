using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IWishlistService
    {
        Task<WishlistDto> GetWishlistAsync(string customerId);
        Task CreateWishlistAsync(string customerId);
    }
}
