using Domain.Entities;
using Domain.IRepositories.IDataRepository;
using Domain.Utilities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.DataRepository
{
    public class ProductRepository :IProductRepository
    {
        private readonly GhostContext _context;

        public ProductRepository(GhostContext context)
        {
            _context = context;
        }
        public async Task<Product?> GetByIdAsync(Guid productId)
        {
            return await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<List<Product>> GetAllProductsAsync(int page, int pageSize)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductVariants)
                    .ThenInclude(pv => pv.ProductColor)
                .Include(p => p.ProductVariants)
                .Include(p => p.ProductVariants)
                    .ThenInclude(pv => pv.ProductImages)
                .Where(p => !p.IsDeleted);

            return await query.ToListAsync();
        }

        public async Task<List<Product>> GetNewProductsAsync(int page, int pageSize)
        {
            var lastMonth = DateTime.UtcNow.AddMonths(-1);
            var query = _context.Products
               // .Include(p => p.Category)
                .Include(p => p.ProductVariants)
                    .ThenInclude(pv => pv.ProductColor)
                .Include(p => p.ProductVariants)
                .Include(p => p.ProductVariants)
                    .ThenInclude(pv => pv.ProductImages)
                .Where(p => p.CreatedOn >= lastMonth && !p.IsDeleted)
                .OrderByDescending(p => p.CreatedOn) // Order by CreatedOn descending to get the newest first.
                .Skip((page - 1) * pageSize) // Apply paging
                .Take(pageSize);

            return await query.ToListAsync(); // Execute the query and return the result as a list.
        }

        public async Task<Product?> GetProductDetailsAsync(Guid productId)
        {
            return await _context.Products
                .Where(p => p.Id == productId && !p.IsDeleted)
                .Include(p => p.Category)
                .Include(p => p.ProductVariants)
                    .ThenInclude(v => v.ProductColor)
                .Include(p => p.ProductVariants)
                    .ThenInclude(v => v.AvailableSizes)
                        .ThenInclude(s => s.Size)
                .Include(p => p.ProductVariants)
                    .ThenInclude(v => v.ProductImages) // Include Product Images
                .FirstOrDefaultAsync();
        }

        public async Task<List<Product>> GetProductsByCategoryAsync(Guid categoryId, int page, int pageSize)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductVariants)
                    .ThenInclude(pv => pv.ProductColor)
                .Include(p => p.ProductVariants)
                .Include(p => p.ProductVariants)
                    .ThenInclude(pv => pv.ProductImages)
                .Where(p => p.CategoryId == categoryId && !p.IsDeleted);

            return await query.ToListAsync();
        }

        public async Task<List<Product>> GetRandomProductsAsync(int count)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductVariants)
                    .ThenInclude(pv => pv.ProductColor)
                .Include(p => p.ProductVariants)
                .Include(p => p.ProductVariants)
                    .ThenInclude(pv => pv.ProductImages)
                .Where(p => !p.IsDeleted)
                .OrderBy(p => Guid.NewGuid()) // Random order
                .Take(count)
                .ToListAsync();
        }


    }
}
