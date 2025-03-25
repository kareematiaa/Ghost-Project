using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories.IDataRepository
{
    public interface IProductVariantRepository
    {
        Task<ProductVariant> GetByIdAsync(Guid id);
        Task UpdateAsync(ProductVariant productVariant);
        Task<IEnumerable<ProductVariant>> GetByIdsAsync(IEnumerable<Guid> ids);
    }
}
