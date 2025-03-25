using Domain.IRepositories.IDataRepository;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.DataRepository
{
    public class AdminDataRepository : IAdminDataRepository
    {
        private readonly GhostContext _context = null!;



        private IProductRepository _productRepository = null!;
        private IWishlistRepository _wishlistRepository = null!;
        private IWishlistItemsRepository _wishlistItemsRepository = null!;
        private ICartRepository _cartRepository = null!;
        private IOrderRepository _orderRepository = null!;
        private IProductVariantRepository _productVariantRepository = null!;
        private IShippingCostRepository _shippingCostRepository = null!;
        private ICustomerRepository _customerRepository = null!;


        public AdminDataRepository(GhostContext context)
        {
            _context = context;
        }

        public ICustomerRepository CustomerRepository
        {
            get
            {
                if (_customerRepository == null)
                    _customerRepository = new CustomerRepository(_context);
                return _customerRepository;
            }
        }

        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                    _productRepository = new ProductRepository(_context);
                return _productRepository;
            }
        }  
        
        public IWishlistRepository WishlistRepository
        {
            get
            {
                if (_wishlistRepository == null)
                    _wishlistRepository = new WishlistRepository(_context);
                return _wishlistRepository;
            }
        }   
        
        public IWishlistItemsRepository WishlistItemsRepository
        {
            get
            {
                if (_wishlistItemsRepository == null)
                    _wishlistItemsRepository = new WishlistitemsRepository(_context);
                return _wishlistItemsRepository;
            }
        }
              
        
        public ICartRepository CartRepository
        {
            get
            {
                if (_cartRepository == null)
                    _cartRepository = new CartRepository(_context);
                return _cartRepository;
            }
        }    
        
        public IOrderRepository OrderRepository
        {
            get
            {
                if (_orderRepository == null)
                    _orderRepository = new OrderRepository(_context);
                return _orderRepository;
            }
        }    
        
        public IShippingCostRepository ShippingCostRepository
        {
            get
            {
                if (_shippingCostRepository == null)
                    _shippingCostRepository = new ShippingCostRepository(_context);
                return _shippingCostRepository;
            }
        }    
        
        public IProductVariantRepository ProductVariantRepository
        {
            get
            {
                if (_productVariantRepository == null)
                    _productVariantRepository = new ProductVariantRepository(_context);
                return _productVariantRepository;
            }
        }

      
    }
}
