using Application.DTOs;
using Application.Extentions;
using Application.IService;
using Application.Services;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ghost.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IAdminDataService _adminDataService;

        public CartController(IAdminDataService adminDataService)
        {
            _adminDataService = adminDataService;
        }


        [HttpPost("AddToCart", Name = "AddToCart")]
        public async Task<ActionResult<APIResponse<string>>> AddToCart( string customerId, Guid productVariantId,int quantity)
        {
            var response = new APIResponse<string>();
            try
            {
                await _adminDataService.CartService.AddToCartAsync(customerId, productVariantId,quantity);
                response.Result = "Added Succfully";
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


        [HttpGet("GetCartItems", Name = "GetCartItems")]
        public async Task<ActionResult<APIResponse<List<CartItemDto>>>> GetCartItems(string customerId)
        {
            var response = new APIResponse<List<CartItemDto>>();
            try
            {
                var data = await _adminDataService.CartService.GetCartItemsAsync(customerId);
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



        [HttpPut("ChangeItemQuantity", Name = "ChangeItemQuantity")]
        public async Task<ActionResult<APIResponse<string>>> ChangeItemQuantity(string customerId, Guid productVariantId, int quantity)
        {
            var response = new APIResponse<string>();
            try
            {
                await _adminDataService.CartService.ChangeItemQuantityAsync(customerId, productVariantId, quantity);
                response.Result = "Added Succfully";
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


        [HttpDelete("empty")]
        public async Task<ActionResult<APIResponse<string>>> EmptyCart(string customerId)
        {
            var response = new APIResponse<string>();
            try
            {
                await _adminDataService.CartService.EmptyCartAsync(customerId);
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


    }
}
