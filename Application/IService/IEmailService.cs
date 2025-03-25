using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IEmailService
    {
        Task SendConfirmationEmail(string userEmail, string otp);
        Task SendResetPassword(string userEmail, string otp);
        Task SendResetPhone(string userEmail, string otp);
    }
}
