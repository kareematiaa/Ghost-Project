using Persistence.OtherConfiguration;
using System.Net.Mail;
using System.Net;
using Domain.Exceptions;
using Domain.External;

namespace Domain.Entities.Other
{
    public class MailingRepository : ISendingRepository
    {
        private readonly MailConfiguration _configuration;

        public MailingRepository(MailConfiguration configuration)
        {
            _configuration = configuration;

            CheckMailParamters();
        }

        private void CheckMailParamters()
        {
            if (_configuration.Server is null || _configuration.SenderEmail is null
                || _configuration.Key is null || _configuration.Password is null
                || _configuration.Port == -1 || _configuration.RestPasswordPath is null
                || _configuration.ConfirmationPath is null)
                throw new SettingsNotFoundException("Mail Parameters Not Found");
        }
        private void InitiateMail(string userMail, string bodyMess, string subject, string fileBodyPath)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(_configuration.SenderEmail);
                mailMessage.To.Add(new MailAddress(userMail));
                mailMessage.Subject = subject;
                if (Path.Exists(fileBodyPath))
                {
                    using (StreamReader reader = File.OpenText(fileBodyPath))
                    {
                        mailMessage.Body = reader.ReadToEnd();
                    }

                    mailMessage.Body = mailMessage.Body
                        .Replace(_configuration.Key, bodyMess);
                    mailMessage.IsBodyHtml = true;
                }
                else
                    mailMessage.Body = bodyMess;

                using (SmtpClient client = new SmtpClient(_configuration.Server, _configuration.Port))
                {
                    client.Credentials = new NetworkCredential(_configuration.SenderEmail, _configuration.Password);
                    client.EnableSsl = true;
                    client.Send(mailMessage);
                }
            }
        }

        public Task SendConfirmation(string user, string otp)
        {
            InitiateMail(user, otp, "Confirmation Account", _configuration.ConfirmationPath);
            return Task.CompletedTask;
        }

        public Task SendResetEmail(string user, string otp)
        {
            throw new NotImplementedException();
        }

        public Task SendResetPassword(string user, string otp)
        {
            InitiateMail(user, otp, "Rest Password", _configuration.RestPasswordPath);
            return Task.CompletedTask;
        }

        public Task SendResetPhone(string user, string otp)
        {
            InitiateMail(user, otp, "Rest Phone", _configuration.ResetPhonePath);
            return Task.CompletedTask;
        }
    }
}
