using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories.IExternalRepository
{
    public interface IAuthenticationRepository
    {
        Task<string> GetRole(string email);
        Task<(string, IAppUser)> Login(string email, string password);
 
        Task Remove(string id);
        Task<string> CustomerRegister(string fullName, string email,
                string phone,  DateTime birthDate, bool gender, string password);

        Task ChangePassword(string userId, string oldPassword, string newPassword);
        Task RestPassword(string token, string email, string newPassword);
        Task<string> RestPassword(string value, bool byEmail = true);

        Task ResetPhone(string token, string userId, string newPhone);
        Task<string> ResetPhone(string userId, string newPhone);

        Task ResetEmail(string token, string userId, string newEmail);
        Task<string> ResetEmail(string userId, string newEmail);
        IEnumerable<Claim> GetClaimsFromToken(string token);
    }
}
