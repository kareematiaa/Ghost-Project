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
    public class ShippingCostRepository :IShippingCostRepository
    {
        private readonly GhostContext _context;

        public ShippingCostRepository(GhostContext context)
        {
            _context = context;
        }

        public async Task<ShippingCost?> GetByMethodAndGovernateAsync(Guid shippingMethodId, string governate)
        {
            return await _context.ShippingCosts
                .Include(sc => sc.ShippingMethod)
                .FirstOrDefaultAsync(sc => sc.ShippingMethodId == shippingMethodId && sc.Governate == governate);
        }

        public async Task<IEnumerable<ShippingCost>> GetAllAsync()
        {
            return await _context.ShippingCosts
                .Include(sc => sc.ShippingMethod)
                .ToListAsync();
        }

        public async Task<List<ShippingCost>> GetAllShippingCostsAsync()
        {
            return await _context.ShippingCosts
                .Include(sc => sc.ShippingMethod)
                .ToListAsync();
        }

        public async Task<ShippingMethod> AddShippingMethodAsync(ShippingMethod method)
        {
            await _context.ShippingMethods.AddAsync(method);
            await _context.SaveChangesAsync();
            return method;
        }

        public async Task<ShippingCost> AddShippingCostAsync(ShippingCost cost)
        {
            await _context.ShippingCosts.AddAsync(cost);
            await _context.SaveChangesAsync();
            return cost;
        }
    }
}
