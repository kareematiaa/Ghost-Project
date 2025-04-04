using Application.DTOs;
using Application.IService;
using AutoMapper;
using Domain.Exceptions;
using Domain.IRepositories.IDataRepository;
using Domain.Utilities;
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

        public ProductService(IAdminDataRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ProductCardDto>> GetAllProductsAsync(int page, int pageSize)
        {
            var pagedResult = await _repository.ProductRepository.GetAllProductsAsync(page, pageSize);
            return _mapper.Map<List<ProductCardDto>>(pagedResult);
        }

        public async Task<List<ProductCardDto>> GetNewProductsAsync(int page, int pageSize)
        {
            var pagedResult = await _repository.ProductRepository.GetNewProductsAsync(page, pageSize);
            return _mapper.Map<List<ProductCardDto>>(pagedResult);
        }

        public async Task<ProductDetailsDto?> GetProductDetailsAsync(Guid productId)
        {
            var product = await _repository.ProductRepository.GetProductDetailsAsync(productId) ?? throw new NotFoundException("product");
           
            return _mapper.Map<ProductDetailsDto>(product);
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
    }
}
