using Application.DTOs;
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
    }
}
