using Domain.Entities;
using Domain.Enums;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context.Users
{
    public class Customer : AppUser, ICustomer
    {
       // public DateOnly DateOfBirth {  get; set; }
       // public Gender Gender {  get; set; }
        public ICollection<Order>? Orders { get; set; }
        public Wishlist Wishlist { get; set; } = null!;
        public Cart Cart { get; set; } = null!;
        public CustomerAddress? CustomerAddress { get; set; }
        public ICollection<PaymentMethod>? PaymentMethods { get; set; }
    }
}
