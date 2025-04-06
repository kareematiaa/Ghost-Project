using Application.DTOs;
using Domain.Enums;
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
        Task<List<ProductColorDto>> GetAllColorsAsync();
        Task<List<ProductSizeDto>> GetAllSizesAsync();
        Task<List<OrderAdminDto>> GetAllOrdersAsync();
        Task<bool> UpdateOrderStatusAsync(Guid orderId , OrderStatus orderStatus);
    }
}
