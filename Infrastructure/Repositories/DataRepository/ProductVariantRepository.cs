using Domain.Entities;
using Domain.IRepositories.IDataRepository;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.DataRepository
{
    public class ProductVariantRepository :IProductVariantRepository
    {
        private readonly GhostContext _context;

        public ProductVariantRepository(GhostContext context)
        {
            _context = context;
        }

        public async Task<ProductVariant> GetByIdAsync(Guid id)
        {
            return await _context.ProductVariants
                .Include(pv => pv.Product)
                .Include(pv => pv.ProductColor)
                .Include(pv => pv.ProductSize)
                .FirstOrDefaultAsync(pv => pv.Id == id);
        }

        public async Task UpdateAsync(ProductVariant productVariant)
        {
            _context.ProductVariants.Update(productVariant);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductVariant>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            return await _context.ProductVariants
                .Include(pv => pv.Product)
                .Include(pv => pv.ProductColor)
                .Include(pv => pv.ProductSize)
                .Where(pv => ids.Contains(pv.Id))
                .ToListAsync();
        }
    }
}
