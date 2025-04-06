using Application.IService;
using AutoMapper;
using Domain.IRepositories.IDataRepository;
using Microsoft.AspNetCore.Http;
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
        private readonly IShippingCostService _shippingCostService;
        private readonly ICustomerService _customerService;
        public AdminDataService(IAdminDataRepository repository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _productService = new ProductService(repository, mapper,httpContextAccessor);
            _wishlistService = new WishlistService(repository, mapper);
            _wishlistItemsService = new WishlistItemsService(repository, mapper,httpContextAccessor);
            _cartService = new CartService(repository, mapper, httpContextAccessor);
            _orderService = new OrderService(repository, mapper);
            _shippingCostService = new ShippingCostService(repository, mapper);
            _customerService = new CustomerService(repository, mapper);

        }
        public IProductService ProductService => _productService;

        public IWishlistService WishlistService => _wishlistService;

        public IWishlistItemsService WishlistItemsService => _wishlistItemsService;
        public ICartService CartService => _cartService;
        public IOrderService OrderService => _orderService;
        public IShippingCostService ShippingCostService => _shippingCostService;
        public ICustomerService CustomerService => _customerService;
    }
}
