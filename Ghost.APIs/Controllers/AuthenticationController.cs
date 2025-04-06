using Application.DTOs;
using Application.DTOs.AuthDTOs;
using Application.IService;
using Application.Services;
using Domain.Enums;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IOtpService _otpService;

        public AuthenticationController(IExternalService service,
              IAdminDataService adminDataService,IOtpService otpService)
        {
            _adminDataService = adminDataService;
            _otpService = otpService;
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

        [HttpPost("AdminRegister")]
        [EnableRateLimiting("FixedWindowPolicy")]
        public async Task<ActionResult> AdminRegister(AdminRegistrationDto model)
        {
            try
            {
                var token = await _service.AuthenticationService.AdminRegister(model);


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

        [HttpPost("generateOtp")]
        public async Task<IActionResult> GenerateOtp([FromQuery] string email, [FromQuery] string purpose = "confirmation")
        {
            if (string.IsNullOrWhiteSpace(email))
                return BadRequest("Email is required");

            var otp = await _otpService.GenerateAndSendOtp(email, purpose);
            return Ok(new { Message = "OTP sent successfully", code = otp });
        }

        [HttpPost("validateOtp")]
        public async Task<IActionResult> ValidateOtp([FromQuery] string email, [FromQuery] string code)
        {
            var result = await _otpService.ValidateOtp(email, code);

            return result switch
            {
                OtpValidationResult.Success => Ok(new { Success = true, Message = "OTP validated successfully" }),
                OtpValidationResult.InvalidCode => BadRequest(new { Success = false, Message = "Invalid OTP code" }),
                OtpValidationResult.AlreadyUsed => BadRequest(new { Success = false, Message = "OTP already used" }),
                OtpValidationResult.NotFound => BadRequest(new { Success = false, Message = "OTP not found or expired" }),
                _ => StatusCode(500, new { Success = false, Message = "Unknown error" })
            };
        }


        [HttpPost("verify")]
    
        public async Task<IActionResult> VerifyToken(string token)
        {
            var userId = User.FindFirst("uid")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var user = await _adminDataService.CartService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(new
            {
                Id = user.Id,
                Data = new
                {
                    FullName = user.FullName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                }
            });
        }
    }
}
