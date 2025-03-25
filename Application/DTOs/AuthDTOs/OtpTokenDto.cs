using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.AuthDTOs
{
    public class OtpTokenDto
    {
        public string Token { get; set; }
        public string Otp { get; set; }
    }
}
