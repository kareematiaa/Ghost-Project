using Application.DTOs.AuthDTOs;
using Application.IService;
using AutoMapper;
using Domain.IRepositories.IDataRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IAdminDataRepository _adminDataRepository;
        private readonly IMapper _mapper;

        public CustomerService(IAdminDataRepository adminDataRepository,IMapper mapper)
        {
            _adminDataRepository = adminDataRepository;
            _mapper = mapper;
        }
        //public Task<CustomerDto> GetCustomerByIdAsync(string id)
        //{
        //    throw new NotImplementedException();
        //}
        public async Task<List<CustomersAdminDto>> GetAllCustomersAsync()
        {
            var customer = await _adminDataRepository.CustomerRepository.GetAllCustomersAsync();
            return _mapper.Map<List<CustomersAdminDto>>(customer);
        }
    }
    
}
