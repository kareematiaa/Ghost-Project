
namespace Domain.External
{
    public interface IMailingRepository
    {
       public void SendEmailConfirmation(string emailData, string fileBodyPath);
       void SendEmailReset(string emailData, string fileBodyPath);
    }
}
