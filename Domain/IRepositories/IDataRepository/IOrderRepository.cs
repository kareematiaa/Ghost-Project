using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories.IDataRepository
{
    public interface IOrderRepository
    {
        Task<Order> GetByIdAsync(Guid id);
        Task<List<Order>> GetCustomerOrdersAsync(string customerId);
        Task<Order> CreateOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task<List<ProductColor>> GetAllColorsAsync();
        Task<List<ProductSize>> GetAllSizesAsync();
        Task<ProductColor> AddColorAsync(ProductColor color);
        Task<ProductSize> AddSizeAsync(ProductSize size);
        Task<List<Order>> GetAllOrdersAsync();
        Task UpdateAsync(Order order);
    }
}
