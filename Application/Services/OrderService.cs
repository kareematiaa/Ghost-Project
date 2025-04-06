using Application.DTOs;
using Application.IService;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.IRepositories.IDataRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IAdminDataRepository _adminDataRepository;
        private readonly IMapper _mapper;

        public OrderService(IAdminDataRepository adminDataRepository, IMapper mapper)
        {
            _adminDataRepository = adminDataRepository;
            _mapper = mapper;
        }

        public async Task<OrderDto> GetOrderDetailsAsync(Guid orderId)
        {
            var order = await _adminDataRepository.OrderRepository.GetByIdAsync(orderId) ?? throw new NotFoundException("order");
            
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<List<OrderSummaryDto>> GetCustomerOrdersAsync(string customerId)
        {
            _ = await _adminDataRepository.CustomerRepository.GetByIdAsync(customerId) ?? throw new NotFoundException("customer");

            var orders = await _adminDataRepository.OrderRepository.GetCustomerOrdersAsync(customerId);
            return _mapper.Map<List<OrderSummaryDto>>(orders);
        }

        public async Task<OrderResultDto> CreateOrderAsync(CreateOrderDto orderDto)
        {
            try
            {
                // Validate product variants and quantities
                var validationResult = await ValidateOrderItems(orderDto.Items);
                if (!validationResult.Success)
                {
                    return validationResult;
                }

                // Calculate shipping cost
                var shippingCost = await _adminDataRepository.ShippingCostRepository.GetByMethodAndGovernateAsync(
                    orderDto.ShippingMethodId, orderDto.Governate);

                if (shippingCost == null)
                {
                    return new OrderResultDto { Success = false, ErrorMessage = "Shipping method not available for this governate" };
                }

                // Create order entity
                var order = _mapper.Map<Order>(orderDto);
                order.OrderStatus = OrderStatus.Ordered;
                order.ShippingCost = shippingCost;

                // Fetch product prices and map order items
                var orderItems = new List<OrderProductVariant>();

                foreach (var item in orderDto.Items)
                {
                    var productVariant = await _adminDataRepository.ProductVariantRepository.GetByIdAsync(item.ProductVariantId);
                    if (productVariant == null)
                    {
                        return new OrderResultDto { Success = false, ErrorMessage = "Invalid product variant." };
                    }

                    var product = await _adminDataRepository.ProductRepository.GetByIdAsync(productVariant.ProductId) ?? throw new NotFoundException("product");
                

                    orderItems.Add(new OrderProductVariant
                    {
                        OrderId = order.Id,
                        ProductVariantId = item.ProductVariantId,
                        Quantity = item.Quantity,
                        SizeId = item.SizeId,
                        Order = order,
                        ProductVariant = productVariant
                    });
                }

                // Assign calculated order items
                order.OrderItems = orderItems;

                // Calculate SUBTOTAL (sum of all order items)
                decimal subtotal = order.OrderItems.Sum(oi => oi.ProductVariant.Product.Price * oi.Quantity);

                // Calculate TOTAL PRICE (subtotal + shipping cost)
                order.TotalPrice = subtotal + shippingCost.Price;

                // Save order
                await _adminDataRepository.OrderRepository.CreateOrderAsync(order);

                // Update product variant quantities
                await UpdateProductVariantQuantities(orderDto.Items);

                return new OrderResultDto { Success = true, OrderId = order.Id };
            }
            catch (Exception ex)
            {
                // Log error
                return new OrderResultDto { Success = false, ErrorMessage = "An error occurred while creating the order" };
            }
        }

        private async Task<OrderResultDto> ValidateOrderItems(IEnumerable<OrderItemDto> items)
        {
            var productVariantIds = items.Select(i => i.ProductVariantId).ToList();
            var productVariants = await _adminDataRepository.ProductVariantRepository.GetByIdsAsync(productVariantIds);

            foreach (var item in items)
            {
                var productVariant = productVariants.FirstOrDefault(pv => pv.Id == item.ProductVariantId) ?? throw new NotFoundException("productVariant");           

                if (productVariant.Quantity < item.Quantity)
                {
                    return new OrderResultDto
                    {
                        Success = false,
                        ErrorMessage = $"Not enough stock for {productVariant.Product.Name}. " +
                                      $"Available: {productVariant.Quantity}, Requested: {item.Quantity}"
                    };
                }           
              
            }

            return new OrderResultDto { Success = true };
        }

        private async Task UpdateProductVariantQuantities(IEnumerable<OrderItemDto> items)
        {
            foreach (var item in items)
            {
                var productVariant = await _adminDataRepository.ProductVariantRepository.GetByIdAsync(item.ProductVariantId);
                productVariant.Quantity -= item.Quantity;
                await _adminDataRepository.ProductVariantRepository.UpdateAsync(productVariant);
            }
        }

        public async Task<List<ProductColorDto>> GetAllColorsAsync()
        {
            var colors = await _adminDataRepository.OrderRepository.GetAllColorsAsync();
            return _mapper.Map<List<ProductColorDto>>(colors);
        }

        public async Task<List<ProductSizeDto>> GetAllSizesAsync()
        {
            var sizes = await _adminDataRepository.OrderRepository.GetAllSizesAsync();
            return _mapper.Map<List<ProductSizeDto>>(sizes);
        }

        public async Task<List<OrderAdminDto>> GetAllOrdersAsync()
        {
            var orders = await _adminDataRepository.OrderRepository.GetAllOrdersAsync();
            return _mapper.Map<List<OrderAdminDto>>(orders);
        }

        public async Task<bool> UpdateOrderStatusAsync(Guid orderId, OrderStatus orderStatus)
        {
            var order = await _adminDataRepository.OrderRepository.GetByIdAsync(orderId)
                ?? throw new NotFoundException("Order not found");

            // Add any business logic/validation here
            if (order.OrderStatus == OrderStatus.Delivered)
            {
                throw new InvalidOperationException("Cannot change status of a delivered order");
            }

            order.OrderStatus = orderStatus;
            await _adminDataRepository.OrderRepository.UpdateAsync(order);
            return true;
        }
    }
}
