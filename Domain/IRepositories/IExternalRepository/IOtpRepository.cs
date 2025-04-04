using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.External
{
    public interface IOtpRepository
    {
        Task<string> GenerateOtp(string email);
        Task<OtpValidationResult> ValidateOtp(string email, string code);
    }
}
