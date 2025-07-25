using Application.DTOs;
using Application.Extentions;
using Application.IService;
using Application.Services;
using Domain.Exceptions;
using Domain.Users;
using Domain.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ghost.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor,IAdminDataService adminDataService) : ControllerBase
    {
        private readonly IWebHostEnvironment _env = env;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IAdminDataService _adminDataService = adminDataService;



        [HttpGet("GetProductOrderVariant", Name = "GetProductOrderVariant")]
        public async Task<ActionResult<APIResponse<ProductVariantOrderDto>>> GetProductOrderVariant(Guid variantId, Guid sizeId)
        {
            var response = new APIResponse<ProductVariantOrderDto>();
            try
            {
                var data = await _adminDataService.ProductService.GetVariantDetailsAsync(variantId,  sizeId);
                response.Result = data;
                response.StatusCode = HttpStatusCode.OK;
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.Message };
                response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }
            return response;
        }

        [HttpGet("GetAllCategories", Name = "GetAllCategories")]
        public async Task<ActionResult<APIResponse<List<CategoryDto>>>> GetAllCategories()
        {
            var response = new APIResponse<List<CategoryDto>>();
            try
            {
                var data = await _adminDataService.ProductService.GetAllCategoriesAsync();
                response.Result = data;
                response.StatusCode = HttpStatusCode.OK;
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.Message };
                response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }
            return response;
        }

        [HttpGet("GetNewProducts", Name = "GetNewProducts")]
        public async Task<ActionResult<APIResponse<List<ProductCardDto>>>> GetNewProducts(int page, int pageSize)
        {
            var response = new APIResponse<List<ProductCardDto>>();
            try
            {
                var data = await _adminDataService.ProductService.GetNewProductsAsync(page, pageSize);
                response.Result = data;
                response.StatusCode = HttpStatusCode.OK;
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.Message };
                response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }
            return response;
        }   
        
        
        [HttpGet("GetAllProducts", Name = "GetAllProducts")]
        public async Task<ActionResult<APIResponse<List<ProductCardDto>>>> GetAllProducts(int page, int pageSize)
        {
            var response = new APIResponse<List<ProductCardDto>>();
            try
            {
                var data = await _adminDataService.ProductService.GetAllProductsAsync(page, pageSize);
                response.Result = data;
                response.StatusCode = HttpStatusCode.OK;
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.Message };
                response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }
            return response;
        }


        [HttpGet("GetRandomProducts", Name = "GetRandomProducts")]
        public async Task<ActionResult<APIResponse<List<ProductCardDto>>>> GetRandomProducts(int count)
        {
            var response = new APIResponse<List<ProductCardDto>>();
            try
            {
                var data = await _adminDataService.ProductService.GetRandomProductsAsync(count);
                response.Result = data;
                response.StatusCode = HttpStatusCode.OK;
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.Message };
                response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }
            return response;
        }


        [HttpGet("GetProductDetails", Name = "GetProductDetails")]
        public async Task<ActionResult<APIResponse<ProductDetailsDto>>> GetProductDetails(Guid productId)
        {
            var response = new APIResponse<ProductDetailsDto>();
            try
            {
                var data = await _adminDataService.ProductService.GetProductDetailsAsync(productId);
                response.Result = data;
                response.StatusCode = HttpStatusCode.OK;
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.Message };
                response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }
            return response;
        }

        [HttpGet("GetProductsByCategory", Name = "GetProductsByCategory")]
        public async Task<ActionResult<APIResponse<List<ProductCardDto>>>> GetProductsByCategory(Guid categoryId,int page, int pageSize)
        {
            var response = new APIResponse<List<ProductCardDto>>();
            try
            {
                var data = await _adminDataService.ProductService.GetProductsByCategoryAsync(categoryId,page, pageSize);
                response.Result = data;
                response.StatusCode = HttpStatusCode.OK;
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.Message };
                response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }
            return response;
        }

        [HttpPost("CreateProduct", Name = "CreateProduct")]
        public async Task<ActionResult<APIResponse<ProductAdminDto>>> CreateProduct(CreateProductDto dto)
        {
            var response = new APIResponse<ProductAdminDto>();
            try
            {
                var data = await _adminDataService.ProductService.CreateProductAsync(dto);
                response.Result = data;
                response.StatusCode = HttpStatusCode.Created;
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.Message };
                response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }
            return response;
        }
        [HttpPost("CreateProductVariants", Name = "CreateProductVariants")]
        public async Task<ActionResult<APIResponse<List<ProductVariantAdminDto>>>> CreateProductVariants([FromBody] CreateProductVariantsDto dto)
        {
            var response = new APIResponse<List<ProductVariantAdminDto>>();
            try
            {
                var data = await _adminDataService.ProductService.CreateProductVariantsAsync(dto.Variants);
                response.Result = data;
                response.StatusCode = HttpStatusCode.Created;
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.Message };
                response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }
            return response;
        }

        //[HttpPost("CreateProductVariant", Name = "CreateProductVariant")]
        //public async Task<ActionResult<APIResponse<ProductVariantAdminDto>>> CreateProductVariant(CreateProductVariantDto dto)
        //{
        //    var response = new APIResponse<ProductVariantAdminDto>();
        //    try
        //    {
        //        var data = await _adminDataService.ProductService.CreateProductVariantAsync(dto);
        //        response.Result = data;
        //        response.StatusCode = HttpStatusCode.Created;
        //        return Ok(response);
        //    }
        //    catch (NotFoundException ex)
        //    {
        //        response.IsSuccess = false;
        //        response.ErrorMessages = new List<string> { ex.Message };
        //        response.StatusCode = HttpStatusCode.NotFound;
        //        return NotFound(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        response.IsSuccess = false;
        //        response.ErrorMessages = new List<string> { ex.Message };
        //    }
        //    return response;
        //}

        [HttpPost("CreateProductVariantImages", Name = "CreateProductVariantImages")]
        public async Task<ActionResult<APIResponse<List<string>>>> CreateProductVariantImages([FromBody] List<ProductImageCreateDto> dtos)
        {
            var response = new APIResponse<List<string>>();
            try
            {
                var data = await _adminDataService.ProductService.UploadImagesAsync(dtos, _env.WebRootPath, GetBaseUrl());
                response.Result = data;
                response.StatusCode = HttpStatusCode.Created;
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.Message };
                response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }
            return response;
        }


        [HttpDelete("DeleteProduct", Name = "DeleteProduct")]
        public async Task<ActionResult<APIResponse<string>>> DeleteProduct(Guid productId)
        {
            var response = new APIResponse<string>();
            try
            {
                var data = await _adminDataService.ProductService.DeleteProductAsync(productId);
                response.Result = "deleted";
                response.StatusCode = HttpStatusCode.NoContent;
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.Message };
                response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }
            return response;
        }


        [HttpDelete("DeleteProductVariant", Name = "DeleteProductVariant")]
        public async Task<ActionResult<APIResponse<string>>> DeleteProductVariant(Guid productVariantId)
        {
            var response = new APIResponse<string>();
            try
            {
                var data = await _adminDataService.ProductService.DeleteProductVariantAsync(productVariantId);
                response.Result = "deleted";
                response.StatusCode = HttpStatusCode.NoContent;
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.Message };
                response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }
            return response;
        }



        [HttpDelete("DeleteProductImage", Name = "DeleteProductImage")]
        public async Task<ActionResult<APIResponse<string>>> DeleteProductImage(Guid productImageId)
        {
            var response = new APIResponse<string>();
            try
            {
                var data = await _adminDataService.ProductService.DeleteProductImageAsync(productImageId);
                response.Result = "deleted";
                response.StatusCode = HttpStatusCode.NoContent;
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.Message };
                response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }
            return response;
        }

        private string GetBaseUrl()
        {
            var request = _httpContextAccessor.HttpContext!.Request;
            return $"{request.Scheme}://{request.Host}";
        }
    }
}
