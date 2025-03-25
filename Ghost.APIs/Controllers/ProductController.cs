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
    public class ProductController(IAdminDataService adminDataService) : ControllerBase
    {
        private readonly IAdminDataService _adminDataService = adminDataService;

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
    }
}
