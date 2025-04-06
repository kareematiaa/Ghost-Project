using Application.DTOs;
using Application.DTOs.AuthDTOs;
using Application.IService;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.IRepositories.IDataRepository;
using Domain.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CartService :ICartService
    {
        private readonly IAdminDataRepository _adminDataRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartService(IAdminDataRepository adminDataRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _adminDataRepository = adminDataRepository;
            _mapper = mapper;
           _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<CartItemDto>> GetCartItemsAsync(string customerId)
        {
            _ = await _adminDataRepository.CustomerRepository.GetByIdAsync(customerId) ?? throw new NotFoundException("customer");

            var cart = await _adminDataRepository.CartRepository.GetCartByCustomerIdAsync(customerId) ?? throw new NotFoundException("cart items");

            return _mapper.Map<List<CartItemDto>>(cart.CartItems, opts =>
            opts.Items["HttpContext"] = _httpContextAccessor.HttpContext);
        }

        public async Task AddToCartAsync(string customerId, Guid productVariantId,Guid sizeId, int quantity)
        {
            _ =await _adminDataRepository.CustomerRepository.GetByIdAsync(customerId) ?? throw new NotFoundException("customer");
            _ = await _adminDataRepository.ProductVariantRepository.GetByIdAsync(productVariantId) ?? throw new NotFoundException("variant");
            _ = await _adminDataRepository.CustomerRepository.GetSizeByIdAsync(sizeId) ?? throw new NotFoundException("size");
            var cart = await _adminDataRepository.CartRepository.GetCartByCustomerIdAsync(customerId) ?? throw new NotFoundException("cart"); ;
         
            var cartItem = new CartItem
            {
                CartId = cart.Id,
                ProductVariantId = productVariantId,
                SizeId = sizeId,
                Quantity = quantity
            };

            await _adminDataRepository.CartRepository.AddToCartAsync(cartItem);
        }

        public async Task ChangeItemQuantityAsync(string customerId, Guid productVariantId,Guid sizeId, int quantity)
        {
            // Retrieve the customer
            var customer = await _adminDataRepository.CustomerRepository.GetByIdAsync(customerId)
                ?? throw new NotFoundException("Customer");

            // Retrieve the customer's cart
            var cart = await _adminDataRepository.CartRepository.GetCartByCustomerIdAsync(customerId)
                ?? throw new NotFoundException("Cart");

            // Retrieve the cart item
            var cartItem = await _adminDataRepository.CartRepository.GetCartItemAsync(cart.Id, productVariantId)
                ?? throw new NotFoundException("Cart item ");

            // Retrieve the product variant
            var productVariant = await _adminDataRepository.ProductVariantRepository.GetByIdAsync(productVariantId)
                ?? throw new NotFoundException("Product variant");

            // Check if the requested quantity exceeds available stock
            if (quantity > productVariant.Quantity)
            {
                throw new NotAllowedException($"Only {productVariant.Quantity} items available in stock");
            }

            // Update the quantity
            cartItem.Quantity = quantity;

            // Save changes
            await _adminDataRepository.CartRepository.UpdateCartItemQuantityAsync(customerId,productVariantId,sizeId,quantity);
        }

        public async Task RemoveItemFromCartAsync(string customerId, Guid productVariantId, Guid sizeId)
        {
            _ = await _adminDataRepository.CustomerRepository.GetByIdAsync(customerId) ?? throw new NotFoundException("customer");
            var cart = await _adminDataRepository.CartRepository.GetCartByCustomerIdAsync(customerId) ?? throw new NotFoundException("cart items");
         
            await _adminDataRepository.CartRepository.RemoveCartItemAsync(customerId,productVariantId,sizeId);
        }

        public async Task EmptyCartAsync(string customerId)
        {
            _ = await _adminDataRepository.CustomerRepository.GetByIdAsync(customerId) ?? throw new NotFoundException("customer");

            var cart = await _adminDataRepository.CartRepository.GetCartByCustomerIdAsync(customerId);
            if (cart == null)
            {
                throw new NotFoundException("Cart");
            }

            await _adminDataRepository.CartRepository.EmptyCartAsync(customerId);
        }

        public async Task<ICustomer> GetUserByIdAsync(string userId)
        {
            return await _adminDataRepository.CustomerRepository.GetByIdAsync(userId);
        }
    }
}
