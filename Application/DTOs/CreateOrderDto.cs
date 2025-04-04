using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CreateOrderDto
    {
        public string CustomerId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public PaymentType PaymentType { get; set; }
        public Guid? PaymentMethodId { get; set; }
        public Guid ShippingMethodId { get; set; }
        public string Governate { get; set; }
        public CustomerAddressDto CustomerAddress { get; set; }
        public IEnumerable<OrderItemDto> Items { get; set; }
    }
}
