using Domain.Entities;
using Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories.IDataRepository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync(int page, int pageSize);
        Task<List<Product>> GetNewProductsAsync(int page, int pageSize);
        Task<Product?> GetProductDetailsAsync(Guid productId);
        Task<List<Product>> GetProductsByCategoryAsync(Guid categoryId, int page, int pageSize);
        Task<List<Product>> GetRandomProductsAsync(int count);
        Task<Product?> GetByIdAsync(Guid productId);
        Task<Product> AddProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);
        Task SoftDeleteProductAsync(Guid productId);
        Task<ProductVariant> AddProductVariantAsync(ProductVariant variant);
        Task SoftDeleteProductVariantAsync(Guid variantId);
        Task AddAsync(ProductImage image);
        Task<ProductVariant?> GetVariantWithDetailsAsync(Guid productVariantId);
        Task<List<Category>> GetAllCategoriesAsync();
        Task<ProductVariant?> GetByIdVariantAsync(Guid productVariantId);
        Task<ProductImage?> GetByIdImageAsync(Guid productImageId);

        Task SoftDeleteProductImageAsync(Guid imageId);
    }

}
