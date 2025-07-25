using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IShippingCostService
    {
        Task<List<ShippingOptionDto>> GetAllShippingOptionsAsync();

        Task<ShippingMethod> AddShippingMethodAsync(ShippingMethodCreateDto dto);

        Task<ShippingCost> AddShippingCostAsync(Guid methodId, ShippingCostCreateDto dto);
    }
}
