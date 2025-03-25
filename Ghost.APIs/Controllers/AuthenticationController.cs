using Application.DTOs;
using Application.DTOs.AuthDTOs;
using Application.IService;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.SignalR;

namespace Ghost.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IExternalService _service;
        private readonly IAdminDataService _adminDataService;
        public AuthenticationController(IExternalService service,
              IAdminDataService adminDataService)
        {
            _adminDataService = adminDataService;
            _service = service;
           
        }


        [HttpPost("Login")]
        [EnableRateLimiting("FixedWindowPolicy")]
        public async Task<ActionResult> Login(LoginDto model)
        {
            try
            {
                var loginResponse = await _service.AuthenticationService.Login(model);           
                return Ok(loginResponse);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (NotAllowedException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (SettingsNotFoundException ex)
            {

                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (AlreadyExistException ex)
            {
                return Conflict(ex.Message);
            }
            catch (IdentityException ex)
            {
                return Problem(ex.Message);
            }
            catch (PropertyException ex)
            {
                return Problem(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpPost("CustomerRegister")]
        [EnableRateLimiting("FixedWindowPolicy")]
        public async Task<ActionResult> Register(CustomerRegisterDto model)
        {
            try
            {
                var token = await _service.AuthenticationService.CustomerRegister(model);

                //Get CustomerId from token and create default closet and cart
                //var customerId = _service.AuthenticationService.GetCustomerIdFromToken(token);
               
              //  await _adminDataService.WishlistService.CreateWishlistAsync(customerId);

                //await _service.PaymentService
                //    .AddCustomer(customerId, model.Email, model.FullName, model.PhoneNumber);

                return Ok(token);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (NotAllowedException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (SettingsNotFoundException ex)
            {

                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (AlreadyExistException ex)
            {
                return Conflict(ex.Message);
            }
            catch (IdentityException ex)
            {
                return Problem(ex.Message);
            }
            catch (PropertyException ex)
            {
                return Problem(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
