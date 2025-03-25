using Domain.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context.Users
{
    public class AppUser : IdentityUser, IAppUser
    {
        public string FullName { get; set; } =null!;
        public DateTime Date { get; set; }
    }

}
