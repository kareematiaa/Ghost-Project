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
    public class WishlistService : IWishlistService
    {
        private readonly IAdminDataRepository _adminDataRepository;
        private readonly IMapper _mapper;

        public WishlistService(IAdminDataRepository adminDataRepository, IMapper mapper)
        {
            _adminDataRepository = adminDataRepository;
            _mapper = mapper;
        }

        public async Task<WishlistDto> GetWishlistAsync(string customerId)
        {
            _ = await _adminDataRepository.CustomerRepository.GetByIdAsync(customerId) ?? throw new NotFoundException("customer");

            var wishlist = await _adminDataRepository.WishlistRepository.GetWishlistByCustomerIdAsync(customerId) ?? throw new NotFoundException("wishlist"); ;
           

            return _mapper.Map<WishlistDto>(wishlist);
        }

        public async Task CreateWishlistAsync(string customerId)
        {
            var wishlist = new Wishlist { CustomerId = customerId };
            await _adminDataRepository.WishlistRepository.CreateWishlistAsync(wishlist);
        }
    }
}
