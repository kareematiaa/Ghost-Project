﻿using Application.DTOs;
using Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IProductService
    {
        Task<List<ProductCardDto>> GetAllProductsAsync(int page, int pageSize);
        Task<List<ProductCardDto>> GetNewProductsAsync(int page, int pageSize);
        Task<ProductDetailsDto?> GetProductDetailsAsync(Guid productId);
        Task<List<ProductCardDto>> GetProductsByCategoryAsync(Guid categoryId, int page, int pageSize);
        Task<List<ProductCardDto>> GetRandomProductsAsync(int count);
        Task<ProductAdminDto> CreateProductAsync(CreateProductDto productDto);
        Task<bool> DeleteProductAsync(Guid productId);
        Task<ProductVariantAdminDto> CreateProductVariantAsync(CreateProductVariantDto variantDto);
        Task<bool> DeleteProductVariantAsync(Guid variantId);
        Task<string> UploadImageAsync(ProductImageCreateDto dto, string rootPath, string baseUrl);
    }

}
