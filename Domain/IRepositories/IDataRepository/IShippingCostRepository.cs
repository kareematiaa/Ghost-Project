using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories.IDataRepository
{
    public interface IShippingCostRepository
    {
        Task<ShippingCost> GetByMethodAndGovernateAsync(Guid shippingMethodId, string governate);
        Task<IEnumerable<ShippingCost>> GetAllAsync();
    }
}
