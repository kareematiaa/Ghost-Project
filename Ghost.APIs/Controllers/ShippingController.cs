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
    public class ShippingController : ControllerBase
    {
        private readonly IAdminDataService _adminDataService;

        public ShippingController(IAdminDataService adminDataService)
        {
            _adminDataService = adminDataService;
        }

        [HttpGet("GetShippingCosts", Name = "GetShippingCosts")]
        public async Task<ActionResult<APIResponse<List<ShippingOptionDto>>>> GetShippingCosts()
        {
            var response = new APIResponse<List<ShippingOptionDto>>();
            try
            {
                var data = await _adminDataService.ShippingCostService.GetAllShippingOptionsAsync();
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
