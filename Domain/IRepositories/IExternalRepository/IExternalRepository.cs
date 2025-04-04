using Domain.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories.IExternalRepository
{
    public interface IExternalRepository
    {
        IAuthenticationRepository AuthenticationRepository { get; }
        ISendingRepository MailingRepository { get; }
       // IPaymentRepository PaymentRepository { get; }
  
    }
}
