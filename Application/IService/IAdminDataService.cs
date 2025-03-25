using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IAdminDataService
    {
        IProductService ProductService { get; }
        IWishlistService WishlistService { get; }
        IWishlistItemsService WishlistItemsService { get; }
        ICartService CartService { get; }
        IOrderService OrderService { get; }

    }
}
