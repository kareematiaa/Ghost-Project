using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories.IDataRepository
{
   public interface IAdminDataRepository
    {
        IProductRepository ProductRepository { get; }
        IWishlistRepository WishlistRepository { get; }
        IWishlistItemsRepository WishlistItemsRepository { get; }
        ICartRepository CartRepository { get; }
        IOrderRepository OrderRepository { get; }
        IProductVariantRepository ProductVariantRepository { get; }
        IShippingCostRepository ShippingCostRepository { get; }
        ICustomerRepository CustomerRepository { get; }

    }
}
