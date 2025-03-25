using Application.DTOs;
using Application.Extentions;
using Application.IService;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ghost.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IAdminDataService _adminDataService;

        public WishlistController(IAdminDataService adminDataService)
        {
            _adminDataService = adminDataService;
        }



        [HttpPost("AddToWishlist", Name = "AddToWishlist")]
        public async Task<ActionResult<APIResponse<string>>> AddToWishlist(Guid productId, string customerId)
        {
            var response = new APIResponse<string>();
            try
            {
                await _adminDataService.WishlistItemsService.AddToWishlistAsync(customerId , productId);
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



        [HttpGet("GetWishlistItems", Name = "GetWishlistItems")]
        public async Task<ActionResult<APIResponse<List<WishlistItemDto>>>> GetWishlistItems(string customerId)
        {
            var response = new APIResponse<List<WishlistItemDto>>();
            try
            {
                var data = await _adminDataService.WishlistItemsService.GetWishlistItemsAsync(customerId);
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


        [HttpDelete("DeleteItemFromWishlist", Name = "DeleteItemFromWishlist")]
        public async Task<ActionResult<APIResponse<string>>> DeleteItemFromWishlist(string customerId,Guid productId)
        {
            var response = new APIResponse<string>();
            try
            {
                await _adminDataService.WishlistItemsService.RemoveFromWishlistAsync(customerId,productId);
                response.StatusCode = HttpStatusCode.NoContent;
                response.Result = "Sadly Deleted 😞";
            }
            catch (NotFoundException ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = [ex.Message];
                response.StatusCode = HttpStatusCode.NotFound;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = [ex.Message];
            }
            return response;
        }
    }
}
