using Application.DTOs.AuthDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IOtpService
    {
        Task<OtpTokenDto> GenerateOtp(string email, string? token = null);
        Task<string> ValidateOtp(string userOtp, string tokenOtp);
    }
}
