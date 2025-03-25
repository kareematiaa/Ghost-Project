using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories.IDataRepository
{
    public interface ICustomerRepository
    {
        Task<ICustomer?> GetByIdAsync(string customerId);
    }
}
