using Domain.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PaymentMethod : BaseEntity
    {
        public string Provider { get; set; } = null!;
        public string? NameOnCard { get; set; }
        [NotMapped]
        public string AccountNumber { get; set; } = null!;
        [NotMapped]
        public int CVC { get; set; }
        public string EncryptedAccountNumber { get; set; } = null!;
        public string EncryptedCVC { get; set; } = null!;
        public ICollection<Order> Orders { get; set; } = null!;
        public DateTime ExpiryDate { get; set; }
        public bool IsDefault { get; set; }
        public string CustomerId { get; set; } = null!;
        public ICustomer Customer { get; set; } = null!;
    }
}
