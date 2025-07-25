using Application.DTOs;
using Application.Extentions;
using Application.IService;
using Domain.Entities;
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


        [HttpPost("AddShippingMethod")]
        public async Task<ActionResult<APIResponse<ShippingMethod>>> AddShippingMethod([FromBody] ShippingMethodCreateDto dto)
        {
            var response = new APIResponse<ShippingMethod>();
            try
            {
                var method = await _adminDataService.ShippingCostService.AddShippingMethodAsync(dto);
                response.Result = method;
                response.StatusCode = HttpStatusCode.Created;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.Message };
                response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode(500, response);
            }
        }

        [HttpPost("AddShippingCost/{methodId}")]
        public async Task<ActionResult<APIResponse<ShippingCost>>> AddShippingCost(Guid methodId, [FromBody] ShippingCostCreateDto dto)
        {
            var response = new APIResponse<ShippingCost>();
            try
            {
                var cost = await _adminDataService.ShippingCostService.AddShippingCostAsync(methodId, dto);
                response.Result = cost;
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
                response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode(500, response);
            }
        }

    }
}
