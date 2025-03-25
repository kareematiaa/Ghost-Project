using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IOrderService
    {
        Task<OrderDto> GetOrderDetailsAsync(Guid orderId);
        Task<List<OrderSummaryDto>> GetCustomerOrdersAsync(string customerId);
        Task<OrderResultDto> CreateOrderAsync(CreateOrderDto orderDto);
    }
}
