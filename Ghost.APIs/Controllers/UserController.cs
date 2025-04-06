using Application.DTOs;
using Application.DTOs.AuthDTOs;
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
    public class UserController : ControllerBase
    {
        private readonly IAdminDataService _adminDataService;

        public UserController(IAdminDataService adminDataService)
        {
            _adminDataService = adminDataService;
        }

        [HttpGet("GetAllCustomers", Name = "GetAllCustomers")]
        public async Task<ActionResult<APIResponse<List<CustomersAdminDto>>>> GetAllCustomers()
        {
            var response = new APIResponse<List<CustomersAdminDto>>();
            try
            {
                var data = await _adminDataService.CustomerService.GetAllCustomersAsync();
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
