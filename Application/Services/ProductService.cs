using Application.DTOs;
using Application.IService;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.IRepositories.IDataRepository;
using Domain.Utilities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService :IProductService
    {
        private readonly IAdminDataRepository _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductService(IAdminDataRepository repository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<ProductCardDto>> GetAllProductsAsync(int page, int pageSize)
        {
            var pagedResult = await _repository.ProductRepository.GetAllProductsAsync(page, pageSize);
            return _mapper.Map<List<ProductCardDto>>(pagedResult, opts =>
            opts.Items["HttpContext"] = _httpContextAccessor.HttpContext);
        }

        public async Task<List<ProductCardDto>> GetNewProductsAsync(int page, int pageSize)
        {
            var pagedResult = await _repository.ProductRepository.GetNewProductsAsync(page, pageSize);
            return _mapper.Map<List<ProductCardDto>>(pagedResult);
        }

        public async Task<ProductDetailsDto?> GetProductDetailsAsync(Guid productId)
        {
            var product = await _repository.ProductRepository.GetProductDetailsAsync(productId) ?? throw new NotFoundException("product");
           
            return _mapper.Map<ProductDetailsDto>(product, opts =>
            opts.Items["HttpContext"] = _httpContextAccessor.HttpContext);
        }

        public async Task<List<ProductCardDto>> GetProductsByCategoryAsync(Guid categoryId, int page, int pageSize)
        {
            var pagedResult = await _repository.ProductRepository.GetProductsByCategoryAsync(categoryId, page, pageSize);
            return _mapper.Map<List<ProductCardDto>>(pagedResult);
        }

        public async Task<List<ProductCardDto>> GetRandomProductsAsync(int count)
        {
            var products = await _repository.ProductRepository.GetRandomProductsAsync(count);
            return _mapper.Map<List<ProductCardDto>>(products);
        }


        public async Task<ProductAdminDto> CreateProductAsync(CreateProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            var createdProduct = await _repository.ProductRepository.AddProductAsync(product);
            return _mapper.Map<ProductAdminDto>(createdProduct);
        }

        public async Task<bool> DeleteProductAsync(Guid productId)
        {
            var product = await _repository.ProductRepository.GetByIdAsync(productId) ?? throw new NotFoundException("product");
            await _repository.ProductRepository.SoftDeleteProductAsync(productId);
            return true;
        }

        //public async Task<ProductVariantAdminDto> CreateProductVariantAsync(CreateProductVariantDto variantDto)
        //{
        //    var variant = _mapper.Map<ProductVariant>(variantDto);
        //    var createdVariant = await _repository.ProductRepository.AddProductVariantAsync(variant);
        //    return _mapper.Map<ProductVariantAdminDto>(createdVariant);
        //}

        public async Task<List<ProductVariantAdminDto>> CreateProductVariantsAsync(List<CreateProductVariantDto> variantDtos)
        {
            var createdVariants = new List<ProductVariantAdminDto>();

            foreach (var dto in variantDtos)
            {
                var variant = _mapper.Map<ProductVariant>(dto);
                var created = await _repository.ProductRepository.AddProductVariantAsync(variant);
                createdVariants.Add(_mapper.Map<ProductVariantAdminDto>(created));
            }

            return createdVariants;
        }

        public async Task<bool> DeleteProductVariantAsync(Guid variantId)
        {
            var variant = await _repository.ProductRepository.GetByIdVariantAsync(variantId) ?? throw new NotFoundException("product variant");
            await _repository.ProductRepository.SoftDeleteProductVariantAsync(variantId);
            return true;
        }


        public async Task<bool> DeleteProductImageAsync(Guid imageId)
        {
            var image = await _repository.ProductRepository.GetByIdImageAsync(imageId) ?? throw new NotFoundException("product image");
            await _repository.ProductRepository.SoftDeleteProductImageAsync(imageId);
            return true;
        }

        public async Task<List<string>> UploadImagesAsync(List<ProductImageCreateDto> dtos, string rootPath, string baseUrl)
        {
            var imageUrls = new List<string>();

            foreach (var dto in dtos)
            {
                byte[] imageBytes = Convert.FromBase64String(dto.Base64Image.Split(',').Last());
                string variantFolder = Path.Combine(rootPath, "Variants", dto.ProductVariantId.ToString());
                Directory.CreateDirectory(variantFolder);

                string fileName = $"{Guid.NewGuid()}.png";
                string filePath = Path.Combine(variantFolder, fileName);

                await File.WriteAllBytesAsync(filePath, imageBytes);

                var imageUrl = $"{baseUrl}/Variants/{dto.ProductVariantId}/{fileName}";
                imageUrls.Add(imageUrl);

                var productImage = new ProductImage
                {
                    Id = Guid.NewGuid(),
                    ProductVariantId = dto.ProductVariantId,
                    URL = $"Variants/{dto.ProductVariantId}/{fileName}",
                    IsDeleted = false
                };

                await _repository.ProductRepository.AddAsync(productImage);
            }

            return imageUrls;
        }




        public async Task<ProductVariantOrderDto?> GetVariantDetailsAsync(Guid productVariantId, Guid sizeId)
        {
            var variant = await _repository.ProductRepository.GetVariantWithDetailsAsync(productVariantId);

            if (variant == null) return null;

            var size = variant.AvailableSizes.FirstOrDefault(s => s.SizeId == sizeId);
            if (size == null) return null;

            return new ProductVariantOrderDto

            {
                
                Id = variant.Id,
                ProductName = variant.Product.Name,
                ProductDescription = variant.Product.Description,
                Price = variant.Product.Price,
                ColorName = variant.ProductColor.Name,
                SizeName = size.Size.Name,
                Quantity = variant.Quantity,
                ImageUrls = variant.ProductImages
                    .Where(img => !img.IsDeleted)
                    .Select(img => img.URL)
                    .ToList()
            };
        }


        public async Task<List<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _repository.ProductRepository.GetAllCategoriesAsync();

            return categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,      
            }).ToList();
        }
    }
}
