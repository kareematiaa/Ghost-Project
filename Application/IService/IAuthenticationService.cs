using Application.DTOs.AuthDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IAuthenticationService
    {
        Task<LoginResponseDto> Login(LoginDto login);

        Task<string> CustomerRegister(CustomerRegisterDto user);

        Task<object> AdminRegister(AdminRegistrationDto admin);

        Task<string> GetCustomerId(string token);

        Task<bool> IsEmailExists(string email);
    }
}
