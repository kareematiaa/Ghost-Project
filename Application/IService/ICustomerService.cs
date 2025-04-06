using Application.DTOs.AuthDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface ICustomerService
    {
       // Task<CustomerDto> GetCustomerByIdAsync(string id);
        Task<List<CustomersAdminDto>> GetAllCustomersAsync();
    }
}
