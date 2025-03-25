﻿using Application.DTOs;
using Application.Extentions;
using Application.IService;
using Application.Services;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ghost.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IAdminDataService _adminDataService;

        public OrderController(IAdminDataService adminDataService)
        {
            _adminDataService = adminDataService;
        }

        [HttpPost("CreateOrder", Name = "CreateOrder")]
        public async Task<ActionResult<APIResponse<OrderResultDto>>> CreateOrder([FromBody] CreateOrderDto orderDto)
        {
            var response = new APIResponse<OrderResultDto>();
            try
            {
                var result = await _adminDataService.OrderService.CreateOrderAsync(orderDto);

                if (!result.Success)
                {
                    response.IsSuccess = false;
                    response.ErrorMessages = new List<string> { result.ErrorMessage };
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(response);
                }

                response.Result = new OrderResultDto { OrderId = result.OrderId };
                response.StatusCode = HttpStatusCode.Created;
                return CreatedAtAction(nameof(GetOrderDetails), new { id = result.OrderId }, response);
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
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }


        [HttpGet("GetOrderDetails/{id}", Name = "GetOrderDetails")]
        public async Task<ActionResult<APIResponse<OrderDto>>> GetOrderDetails(Guid id)
        {
            var response = new APIResponse<OrderDto>();
            try
            {
                var orderDetails = await _adminDataService.OrderService.GetOrderDetailsAsync(id);

                response.Result = orderDetails;
                response.StatusCode = HttpStatusCode.OK;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.Message };
                response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetCustomerOrders/{customerId}", Name = "GetCustomerOrders")]
        public async Task<ActionResult<APIResponse<List<OrderSummaryDto>>>> GetCustomerOrders(string customerId)
        {
            var response = new APIResponse<List<OrderSummaryDto>>();
            try
            {
                var orders = await _adminDataService.OrderService.GetCustomerOrdersAsync(customerId);

                response.Result = orders;
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
                response.StatusCode = HttpStatusCode.InternalServerError;
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }


    

}
}
