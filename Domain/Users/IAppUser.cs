using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users
{
    public interface IAppUser  
    {
        public string Id { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string FullName { get; set; } 
        public DateTime Date { get; set; }

    }
}
