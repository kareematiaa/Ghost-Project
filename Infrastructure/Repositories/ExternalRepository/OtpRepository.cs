using Domain.Exceptions;
using Domain.External;
using Microsoft.IdentityModel.Tokens;
using Persistence.ExternalConfiguration;
using AppUtility = Domain.Utilities.ClaimsUtility;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.ExternalRepository
{
    public class OtpRepository : IOtpRepository
    {
        private readonly JWTConfiguration _configuration;
        public OtpRepository(JWTConfiguration configuration)
        {
            _configuration = configuration;
            CheckJWTParamters();
        }

        private void CheckJWTParamters()
        {
            if (_configuration.Key is null || _configuration.Issuer is null
                || _configuration.Audience is null || _configuration.LoginDays == -1)
                throw new SettingsNotFoundException("JWT Paramters Not Found");
        }
        public Task<string> GenerateOTP()
        {
            return Task.FromResult(RandomNumberGenerator.GetHexString(6));
        }

        public Task<string> GenerateOTPToken(string otp, string? userEmail = null, string? token = null)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("validTime", _configuration.OtpMinutes.ToString()),
                new Claim(AppUtility.Otp, otp),
                userEmail is null? new Claim(JwtRegisteredClaimNames.Email, ""): new Claim(JwtRegisteredClaimNames.Email, userEmail),
                token is null? new Claim(AppUtility.Token, "") : new Claim(AppUtility.Token, token),
            }; 
            var jwtToken = new JwtSecurityToken(_configuration.Issuer, _configuration.Audience, claims,
                expires: DateTime.Now.AddMinutes(_configuration.OtpMinutes),
                signingCredentials: credentials);

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(jwtToken));
        }

    }
}
