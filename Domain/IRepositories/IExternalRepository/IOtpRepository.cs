using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.External
{
    public interface IOtpRepository
    {
        Task<string> GenerateOTP();
        Task<string> GenerateOTPToken(string otp,
            string? userEmail = null, string? token = null);
    }
}
