using Domain.IRepositories.IExternalRepository;
using Domain.Users;
using Infrastructure.Context;
using Infrastructure.Context.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using AppUtility = Domain.Utilities.ClaimsUtility;

using Persistence.ExternalConfiguration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Exceptions;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Application.DTOs.AuthDTOs;

namespace Infrastructure.Repositories.ExternalRepository
{
    public class AuthenticationRepository :IAuthenticationRepository
    {
        private readonly JWTConfiguration _jwtConfig;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly GhostContext _db;

        public AuthenticationRepository(JWTConfiguration configuration,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            GhostContext db)
        {
            _jwtConfig = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;

        }

        private async Task<string> GenerateJwtToken(AppUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in userRoles)
                userClaims.Add(new(AppUtility.Role, role));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
        {
            new Claim(AppUtility.Id, user.Id),
            new Claim("fullName", user.FullName),
            new Claim("email", user.Email),
            new Claim("phoneNumber", user.PhoneNumber),
      
            
            new Claim(JwtRegisteredClaimNames.AuthTime,
                    _jwtConfig.LoginDays.ToString())
        }
            .Union(userClaims)
            .Union(roleClaims); ;

            var jwtToken = new JwtSecurityToken(_jwtConfig.Issuer, _jwtConfig.Audience, claims,
                 expires: DateTime.Now.AddDays(_jwtConfig.LoginDays),
                 signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        private string FillError(IdentityResult result)
        {
            var errors = "";
            foreach (var error in result.Errors)
            {
                errors += $"{error.Description} , ";
            }
            return errors;
        }

        private async Task AddRole(AppUser user, string roleName)
        {
            var res = await _userManager
               .AddToRoleAsync(user, roleName);
            if (!res.Succeeded)
                throw new IdentityException(FillError(res));
        }

        public async Task<string> GetRole(string email)
        {
            var user = await _userManager.FindByEmailAsync(email) ?? throw new UnauthorizedAccessException("User not found");
            var roles = await _userManager.GetRolesAsync(user);
            return roles.FirstOrDefault() ?? "User";
        }

        private async Task AddClaimms(AppUser user)
        {
            var res = await _userManager.AddClaimAsync(user, new(ClaimTypes.NameIdentifier, user.Id));
            if (!res.Succeeded)
                throw new IdentityException(FillError(res));
        }

        private async Task<AppUser> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new NotFoundException("User");
            return user;
        }

        private async Task<AppUser> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                throw new NotFoundException("User");
            return user;
        }

        private async Task<AppUser> GetUserByPhone(string phone)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.PhoneNumber == phone);
            if (user == null)
                throw new NotFoundException("User");
            return user;
        }

        public async Task<(string, IAppUser)> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || !(await _userManager.CheckPasswordAsync(user, password)))
                throw new UnauthorizedAccessException("email or password is incorrect");
            if (password.Length != 0)
            {
                if (!await _userManager.CheckPasswordAsync(user, password))
                    throw new UnauthorizedAccessException("Email or Password is Incorrect");

                var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

                if (result.IsNotAllowed)
                    throw new NotAllowedException("User");
                if (!result.Succeeded)
                    throw new IdentityException("Error in LogIn");
            }

            var token = await GenerateJwtToken(user);

            return (token, user);
        }

        public async Task<object> AdminRegister(
                             string fullName, string email, string phone, string password)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null) throw new AlreadyExistException(email);

            var user = new Admin
            {
                UserName = email,
                Email = email,
                PhoneNumber = phone,
                FullName = fullName,
            
            };

            user.UserName = user.Email;

            IdentityResult res = await _userManager.CreateAsync(user);
            if (!res.Succeeded)
                throw new IdentityException(FillError(res));

            if (!string.IsNullOrEmpty(password))
            {
                res = await _userManager.AddPasswordAsync(user, password);
                if (!res.Succeeded)
                    throw new IdentityException(FillError(res));
            }

            await AddClaimms(user);
            await AddRole(user, UserRole.Admin.ToString());

            var jwtToken = await GenerateJwtToken(user);

            return new
            {
                Id = user.Id,
                Token = jwtToken,
                Role = UserRole.Admin.ToString(),
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
               // DateOfBirth = user.DateOfBirth,
                //Gender = user.Gender.ToString()
            };
        }

        public async Task<object> CustomerRegister(
                         string fullName, string email, string phone, string password)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null) throw new AlreadyExistException(email);

            var user = new Customer
            {
                UserName = email,
                Email = email,
                PhoneNumber = phone,
                FullName = fullName,
                //DateOfBirth = birthDate,
                //  Gender = gender ? Gender.Male : Gender.FeMale,
            };

            user.UserName = user.Email;

            IdentityResult res = await _userManager.CreateAsync(user);
            if (!res.Succeeded)
                throw new IdentityException(FillError(res));

            if (!string.IsNullOrEmpty(password))
            {
                res = await _userManager.AddPasswordAsync(user, password);
                if (!res.Succeeded)
                    throw new IdentityException(FillError(res));
            }

            await AddClaimms(user);
            await AddRole(user, UserRole.Customer.ToString());

            var jwtToken = await GenerateJwtToken(user);

            return new
            {
                Id = user.Id,
                Token = jwtToken,
                Role = UserRole.Customer.ToString(),
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                // DateOfBirth = user.DateOfBirth,
                //Gender = user.Gender.ToString()
            };
        }

        public async Task ChangePassword(string userId, string oldPassword, string newPassword)
        {
            var user = await GetUserById(userId);

            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (!result.Succeeded)
                throw new UnauthorizedException(FillError(result));
        }

        public async Task Remove(string id)
        {
            var user = await GetUserById(id);

            if (user != null)
                await _userManager.DeleteAsync(user);
        }

        public async Task<string> RestPassword(string value, bool byEmail = true)
        {
            var user = byEmail ? await GetUserByEmail(value) : await GetUserByPhone(value);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            return token;
        }

        public async Task RestPassword(string token, string email, string newPassword)
        {
            var user = await GetUserByEmail(email);

            await _userManager.ResetPasswordAsync(user, token, newPassword);
        }

        public async Task<string> ResetPhone(string userId, string newPhone)
        {
            var user = await GetUserById(userId);
            return await _userManager.GenerateChangePhoneNumberTokenAsync(user, newPhone);
        }

        public async Task ResetPhone(string token, string userId, string newPhone)
        {
            var user = await GetUserById(userId);

            var result = await _userManager.ChangePhoneNumberAsync(user, newPhone, token);

            if (!result.Succeeded)
                throw new IdentityException(FillError(result));
        }

        public async Task<string> ResetEmail(string userId, string newEmail)
        {
            var user = await GetUserById(userId);
            return await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
        }

        public async Task ResetEmail(string token, string userId, string newEmail)
        {
            var user = await GetUserById(userId);

            var result = await _userManager.ChangeEmailAsync(user, newEmail, token);

            if (!result.Succeeded)
                throw new IdentityException(FillError(result));
        }

        public IEnumerable<Claim> GetClaimsFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null)
                throw new ArgumentException("Invalid token");

            if (jwtToken.ValidTo.ToLocalTime() < DateTime.Now)
                throw new UnauthorizedAccessException("Token is Expired");

            return jwtToken.Claims;
        }
    }
}
