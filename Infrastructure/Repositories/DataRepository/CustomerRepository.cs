using Application.DTOs.AuthDTOs;
using Domain.Entities;
using Domain.IRepositories.IDataRepository;
using Domain.Users;
using Infrastructure.Context;
using Infrastructure.Context.Users;
using Microsoft.AspNetCore.Identity;
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
        public async Task<IReadOnlyList<ICustomer>> GetAllCustomersAsync()
        {
            return await _context.Users
                .OfType<Customer>()
                .Include(c => c.CustomerAddress)
                .ToListAsync<ICustomer>();
        }

        public async Task<ProductSize?> GetSizeByIdAsync(Guid Id)
        {
            return await _context.ProductSizes.FirstOrDefaultAsync(c => c.Id == Id);
        }
    }
}
