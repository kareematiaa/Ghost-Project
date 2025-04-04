using Application.DTOs;
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
    }
}
