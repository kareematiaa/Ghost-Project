using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users
{
    public interface ICustomer : IAppUser
    {
        // DateOnly DateOfBirth { get; set; }    
       //  Gender Gender { get; set; }
         ICollection<Order>? Orders { get; set; }
         Wishlist Wishlist { get; set; } 
         Cart Cart { get; set; } 
         CustomerAddress? CustomerAddress { get; set; }
         ICollection<PaymentMethod>? PaymentMethods { get; set; }

    }
}
