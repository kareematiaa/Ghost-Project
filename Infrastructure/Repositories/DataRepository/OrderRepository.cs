using Domain.Entities;
using Domain.IRepositories.IDataRepository;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.DataRepository
{
    public class OrderRepository :IOrderRepository
    {
        private readonly GhostContext _context;

        public OrderRepository(GhostContext context)
        {
            _context = context;
        }
        public async Task<Order> GetByIdAsync(Guid id)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.ProductVariant)
                        .ThenInclude(pv => pv.Product)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.ProductVariant)
                        .ThenInclude(pv => pv.ProductColor)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.ProductVariant)
                .Include(o => o.CustomerAddress)
                .Include(o => o.ShippingCost)
                .Include(o => o.PaymentMethod)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<List<Order>> GetCustomerOrdersAsync(string customerId)
        {
            return await _context.Orders
                .Where(o => o.CustomerId == customerId)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.ProductVariant)
                        .ThenInclude(pv => pv.Product)
                .Include(o => o.CustomerAddress)
                .Include(o => o.ShippingCost)
                .OrderByDescending(o => o.Date)
                .ToListAsync();
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ProductColor>> GetAllColorsAsync()
        {
            return await _context.ProductColors.ToListAsync();
        }

        public async Task<List<ProductSize>> GetAllSizesAsync()
        {
            return await _context.ProductSizes.ToListAsync();
        }

        public async Task<ProductColor> AddColorAsync(ProductColor color)
        {
            await _context.ProductColors.AddAsync(color);
            await _context.SaveChangesAsync();
            return color;
        }

        public async Task<ProductSize> AddSizeAsync(ProductSize size)
        {
            await _context.ProductSizes.AddAsync(size);
            await _context.SaveChangesAsync();
            return size;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.ProductVariant)
                .ToListAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
    }
}
