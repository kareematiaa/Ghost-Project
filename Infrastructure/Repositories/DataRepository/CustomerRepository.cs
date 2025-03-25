using Domain.IRepositories.IDataRepository;
using Domain.Users;
using Infrastructure.Context;
using Infrastructure.Context.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.DataRepository
{
    public class CustomerRepository :ICustomerRepository
    {
        private readonly  GhostContext _context;

        public CustomerRepository(GhostContext context)
        {
            _context = context;
        }

        public async Task<ICustomer?> GetByIdAsync(string customerId)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
        }
    }
}
