using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context.Users
{
    public class Admin : AppUser ,IAdmin
    {
        public new int Id { get; set; }
    }
}
