using Application.DTOs;
using Application.IService;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.IRepositories.IDataRepository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class WishlistItemsService :IWishlistItemsService
    {
        private readonly IAdminDataRepository _adminDataRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WishlistItemsService(IAdminDataRepository adminDataRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _adminDataRepository = adminDataRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task AddToWishlistAsync(string customerId, Guid productId)
        {
            _ = await _adminDataRepository.CustomerRepository.GetByIdAsync(customerId) ?? throw new NotFoundException("customer");
            _ = await _adminDataRepository.ProductRepository.GetByIdAsync(productId) ?? throw new NotFoundException("product");

            var wishlist = await _adminDataRepository.WishlistRepository.GetWishlistByCustomerIdAsync(customerId) ?? throw new NotFoundException("wishlist"); ;
          

            var wishlistItem = new WishlistItem
            {
                WishlistId = wishlist.Id,
                ProductId = productId
            };

            await _adminDataRepository.WishlistItemsRepository.AddToWishlistAsync(wishlistItem);
        }

        public async Task<List<WishlistItemDto>> GetWishlistItemsAsync(string customerId)
        {
            _ = await _adminDataRepository.CustomerRepository.GetByIdAsync(customerId) ?? throw new NotFoundException("customer");

            var wishlist = await _adminDataRepository.WishlistRepository.GetWishlistByCustomerIdAsync(customerId) ?? throw new NotFoundException("wishlist"); ;
        
            var wishlistItems = await _adminDataRepository.WishlistItemsRepository.GetWishlistItemsAsync(wishlist.Id) ?? throw new NotFoundException("wishlist items");
            return _mapper.Map<List<WishlistItemDto>>(wishlistItems, opts =>
            opts.Items["HttpContext"] = _httpContextAccessor.HttpContext);
        }


        public async Task RemoveFromWishlistAsync(string customerId, Guid productId)
        {
            _ = await _adminDataRepository.CustomerRepository.GetByIdAsync(customerId) ?? throw new NotFoundException("customer");

            var wishlist = await _adminDataRepository.WishlistRepository.GetWishlistByCustomerIdAsync(customerId) ?? throw new NotFoundException("wishlist"); ;
        

            await _adminDataRepository.WishlistItemsRepository.RemoveFromWishlistAsync(wishlist.Id, productId);
        }
    }
}
