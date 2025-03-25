using Application.IService;
using AutoMapper;
using Domain.IRepositories.IExternalRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ExternalService :IExternalService
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IEmailService _emailService;
        private readonly IOtpService _otpService;
        private readonly IExternalRepository _repository;
        private readonly IAdminDataService _service;
        private readonly IMapper _mapper;

        public ExternalService(IExternalRepository repository, IAdminDataService service, IMapper mapper)
        {
            _authenticationService = new AuthenticationService(repository, mapper, service);
            _emailService = new EmailService(repository);
            _otpService = new OtpService(repository);
            _repository = repository;
            _service = service;
            _mapper = mapper;
        }
        public IAuthenticationService AuthenticationService => _authenticationService;

        public IEmailService EmailService => _emailService;

        public IOtpService OtpService => _otpService;
    }
}
