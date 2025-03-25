using Application.DTOs.AuthDTOs;
using Application.IService;
using Domain.Exceptions;
using Domain.IRepositories.IExternalRepository;
using Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OtpService :IOtpService
    {
        private readonly IExternalRepository _repository;

        public OtpService(IExternalRepository repository)
        {
            _repository = repository;
        }


        private string ExtractOtp(string token)
        {
            var claims = _repository.AuthenticationRepository.GetClaimsFromToken(token);

            return claims.First(claim => claim.Type == ClaimsUtility.Otp).Value;
        }

        public async Task<OtpTokenDto> GenerateOtp(string email, string? token = null)
        {
            string otp = await _repository.OtpRepository.GenerateOTP();
            //return new(await _repository.OtpRepository
            //        .GenerateOTPToken(otp, email, token), otp);
            return null;
        }

        public Task<string> ValidateOtp(string userOtp, string tokenOtp)
        {
            string res = ExtractOtp(tokenOtp);

            if (!res.Equals(userOtp))
                throw new TokenNotValidException("Otp not Valid");

            return Task.FromResult(res);
        }
    }
}
