using Application.DTOs;
using Application.IService;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.IRepositories.IDataRepository;
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

        public CartService(IAdminDataRepository adminDataRepository, IMapper mapper)
        {
            _adminDataRepository = adminDataRepository;
            _mapper = mapper;
        }

        public async Task<List<CartItemDto>> GetCartItemsAsync(string customerId)
        {
            _ = await _adminDataRepository.CustomerRepository.GetByIdAsync(customerId) ?? throw new NotFoundException("customer");

            var cart = await _adminDataRepository.CartRepository.GetCartByCustomerIdAsync(customerId)  ?? throw new NotFoundException("cart items");
            
            return _mapper.Map<List<CartItemDto>>(cart.CartItems);
        }

        public async Task AddToCartAsync(string customerId, Guid productVariantId, int quantity)
        {
            _ =await _adminDataRepository.CustomerRepository.GetByIdAsync(customerId) ?? throw new NotFoundException("customer");
            _ = await _adminDataRepository.ProductVariantRepository.GetByIdAsync(productVariantId) ?? throw new NotFoundException("variant");
            var cart = await _adminDataRepository.CartRepository.GetCartByCustomerIdAsync(customerId) ?? throw new NotFoundException("cart"); ;
         
            var cartItem = new CartItem
            {
                CartId = cart.Id,
                ProductVariantId = productVariantId,
                Quantity = quantity
            };

            await _adminDataRepository.CartRepository.AddToCartAsync(cartItem);
        }

        public async Task ChangeItemQuantityAsync(string customerId, Guid productVariantId, int quantity)
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
            await _adminDataRepository.CartRepository.UpdateCartItemAsync(cartItem);
        }

        public async Task RemoveItemFromCartAsync(string customerId, Guid cartItemId)
        {
            _ = await _adminDataRepository.CustomerRepository.GetByIdAsync(customerId) ?? throw new NotFoundException("customer");
            var cart = await _adminDataRepository.CartRepository.GetCartByCustomerIdAsync(customerId) ?? throw new NotFoundException("cart items");
         
            await _adminDataRepository.CartRepository.RemoveFromCartAsync(cartItemId);
        }

        public async Task EmptyCartAsync(string customerId)
        {
            _ = await _adminDataRepository.CustomerRepository.GetByIdAsync(customerId) ?? throw new NotFoundException("customer");

            var cart = await _adminDataRepository.CartRepository.GetCartByCustomerIdAsync(customerId);
            if (cart == null)
            {
                throw new NotFoundException("Cart not found.");
            }

            await _adminDataRepository.CartRepository.EmptyCartAsync(cart.Id);
        }
    }
}
