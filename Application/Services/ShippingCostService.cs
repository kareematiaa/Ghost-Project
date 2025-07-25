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
    public class ShippingCostService :IShippingCostService
    {
        private readonly IAdminDataRepository _adminDataRepository;
        private readonly IMapper _mapper;

        public ShippingCostService(IAdminDataRepository adminDataRepository ,IMapper mapper)
        {
            _adminDataRepository = adminDataRepository;
            _mapper = mapper;
        }

        public async Task<List<ShippingOptionDto>> GetAllShippingOptionsAsync()
        {
            var shippingCosts = await _adminDataRepository.ShippingCostRepository.GetAllShippingCostsAsync();

            return _mapper.Map<List<ShippingOptionDto>>(shippingCosts);
        }

        public async Task<ShippingMethod> AddShippingMethodAsync(ShippingMethodCreateDto dto)
        {
            var method = new ShippingMethod
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                EstimatedDelivery = dto.EstimatedDelivery,
             
            };

            return await _adminDataRepository.ShippingCostRepository.AddShippingMethodAsync(method);
        }

        public async Task<ShippingCost> AddShippingCostAsync(Guid methodId, ShippingCostCreateDto dto)
        {
           

            var cost = new ShippingCost
            {
                ShippingMethodId = methodId,
                Governate = dto.Governate,
                Price = dto.Price
            };

            return await _adminDataRepository.ShippingCostRepository.AddShippingCostAsync(cost);
        }

    }
}
