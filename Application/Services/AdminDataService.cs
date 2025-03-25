using Application.IService;
using AutoMapper;
using Domain.IRepositories.IDataRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AdminDataService :IAdminDataService
    {
        private readonly IProductService _productService;
        private readonly IWishlistService _wishlistService;
        private readonly IWishlistItemsService _wishlistItemsService;
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        public AdminDataService(IAdminDataRepository repository, IMapper mapper)
        {
            _productService = new ProductService(repository, mapper);
            _wishlistService = new WishlistService(repository, mapper);
            _wishlistItemsService = new WishlistItemsService(repository, mapper);
            _cartService = new CartService(repository, mapper);
            _orderService = new OrderService(repository, mapper);

        }
        public IProductService ProductService => _productService;

        public IWishlistService WishlistService => _wishlistService;

        public IWishlistItemsService WishlistItemsService => _wishlistItemsService;
        public ICartService CartService => _cartService;
        public IOrderService OrderService => _orderService;
    }
}
