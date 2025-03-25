using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class PaymentFailedException : Exception
    {
        public PaymentFailedException(string mess) :
            base(mess)
        { }
        public PaymentFailedException(string entityName, string messege): 
        base($"{entityName}  already exist, and the Identiy Messege are ")
    {

    }
    }
}
