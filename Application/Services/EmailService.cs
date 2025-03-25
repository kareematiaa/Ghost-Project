using Application.IService;
using Domain.IRepositories.IExternalRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    class EmailService :IEmailService
    {
        private readonly IExternalRepository _repository;

        public EmailService(IExternalRepository repository)
        {
            _repository = repository;
        }

        public Task SendConfirmationEmail(string userEmail, string otp)
        {
            return _repository.MailingRepository.SendConfirmation(userEmail, otp);

        }

        public Task SendResetPassword(string userEmail, string otp)
        {
            return _repository.MailingRepository
                .SendResetPassword(userEmail, otp);
        }

        public Task SendResetPhone(string userEmail, string otp)
        {
            return _repository.MailingRepository.SendResetPhone(userEmail, otp);
        }
    }
}
