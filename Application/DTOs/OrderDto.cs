using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal? Discount { get; set; }
        public string OrderStatus { get; set; }
        public string PaymentType { get; set; }
        public string ShippingMethod { get; set; }
        public decimal ShippingCost { get; set; }
        public CustomerAddressDto CustomerAddress { get; set; }
        public IEnumerable<OrderItemDto> OrderItems { get; set; }
    }
}
