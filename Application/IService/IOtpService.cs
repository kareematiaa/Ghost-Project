using Application.DTOs.AuthDTOs;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IOtpService
    {
        Task<string> GenerateAndSendOtp(string email, string purpose);
        Task<OtpValidationResult> ValidateOtp(string email, string code);
    }
}
