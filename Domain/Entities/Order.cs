using Domain.Enums;
using Domain.Users;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order : BaseEntity
    {
        public DateTime Date { get; private set; } = DateTime.Now;
        public decimal TotalPrice { get; set; }
        public decimal? Discount { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public PaymentType PaymentType { get; set; }
        //public string? TransactionId { get; set; }

        public virtual Guid? PaymentMethodId { get; set; }
        public string Governate { get; set; } = null!;
        public virtual Guid ShippingMethodId { get; set; }

        public string CustomerId { get; set; } = null!;
        public ICustomer Customer { get; set; } = null!;
        public CustomerAddress CustomerAddress { get; set; } = null!;
        public virtual ShippingCost ShippingCost { get; set; } = null!;
        public virtual PaymentMethod? PaymentMethod { get; set; }   
        public ICollection<OrderProductVariant> OrderItems { get; set; } = null!;
    }
}
