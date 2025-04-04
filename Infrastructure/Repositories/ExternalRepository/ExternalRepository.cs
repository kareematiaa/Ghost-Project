using Domain.Entities.Other;
using Domain.External;
using Domain.IRepositories.IExternalRepository;
using Infrastructure.Context;
using Infrastructure.Context.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Persistence.ExternalConfiguration;
using Persistence.OtherConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.ExternalRepository
{
    public class ExternalRepository: IExternalRepository
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly GhostContext _db;
        private IAuthenticationRepository _authenicationRepository;
        private ISendingRepository _mailingRepository;
        private IOtpRepository _otpRepository;

        public ExternalRepository(IConfiguration configuration,
            UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, GhostContext db)
        {
           _configuration = configuration;
           _userManager = userManager;
           _signInManager = signInManager;
           _db = db;
        }

        public IAuthenticationRepository AuthenticationRepository
        {
            get
            {
                if (_authenicationRepository == null)
                {
                    JWTConfiguration jwtConfiguration = new JWTConfiguration();
                   

                    _configuration.GetRequiredSection("Jwt").Bind(jwtConfiguration);

                    _authenicationRepository = new AuthenticationRepository(jwtConfiguration, _userManager,
                       _signInManager, _db);
                }
                return _authenicationRepository;
            }
        }

        public ISendingRepository MailingRepository
        {
            get
            {
                if (_mailingRepository == null)
                {
                    MailConfiguration mailConfiguration = new MailConfiguration();

                    _configuration.GetRequiredSection("EmailConfiguration").Bind(mailConfiguration);
                    _mailingRepository = new MailingRepository(mailConfiguration);
                }
                return _mailingRepository;
            }
        }

    
    }
}
