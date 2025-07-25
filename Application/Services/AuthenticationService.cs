﻿using Application.DTOs.AuthDTOs;
using Application.IService;
using AutoMapper;
using Domain.Exceptions;
using Domain.IRepositories.IExternalRepository;
using Domain.Users;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IExternalRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAdminDataService _adminService;

        public AuthenticationService(IExternalRepository repository, IMapper mapper, IAdminDataService adminService)
        {
            _repository = repository;
            _mapper = mapper;
            _adminService = adminService;
        }


        private object ConvertUser(string role, IAppUser data)
        {
            return role switch
            {
                "Customer" => _mapper.Map<CustomerDto>(data),
                "Admin" => _mapper.Map<AdminDto>(data),
                _ => _mapper.Map<CustomerDto>(data)
            };
        }


        public async Task<string> CustomerRegister(CustomerRegisterDto user)
        {
            var registerResponse = await _repository.AuthenticationRepository.CustomerRegister(
                fullName: user.FullName,
                email: user.Email,
                phone: user.PhoneNumber,
                //birthDate: user.DateOfBirth,
                //gender: user.Gender,
                password: user.Password
            );

            return registerResponse; // Since CustomerRegister now returns an object instead of just a token
        }

        public async Task<object> AdminRegister(AdminRegistrationDto admin)
        {
            var registerResponse = await _repository.AuthenticationRepository.AdminRegister(
                fullName: admin.FullName,
                email: admin.Email,
                phone: admin.PhoneNumber,
                password: admin.Password
            );

            return registerResponse; // Since CustomerRegister now returns an object instead of just a token
        }

        public async Task<LoginResponseDto> Login(LoginDto login)
        {
            var result = await _repository.AuthenticationRepository
           .Login(login.Email, login.Password);

            var loginResponseDto = new LoginResponseDto();

            loginResponseDto.Token = result.Item1;
            loginResponseDto.Role = await _repository.AuthenticationRepository
                .GetRole(login.Email) ?? throw new NotFoundException("Role");

            loginResponseDto.Id = result.Item2.Id;
            loginResponseDto.Data = ConvertUser(loginResponseDto.Role, result.Item2);

            return loginResponseDto;
        }

        public async Task<string> GetCustomerId(string token)
        {
       
            var customerId = _repository.AuthenticationRepository.GetCustomerIdFromTokenn(token);
            if (string.IsNullOrEmpty(customerId))
            {
                throw new NotFoundException("Customer ID not found in the token.");
            }
            return customerId;

        }

        public async Task<bool> IsEmailExists(string email)
        {
            return await _repository.AuthenticationRepository.IsEmailExists(email);

        }
    }
}
