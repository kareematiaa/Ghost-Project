using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class ExternalAuthenticationException : Exception
    {
        public ExternalAuthenticationException()  :base(){ }
        public ExternalAuthenticationException(string provider) : base($"{provider} External Authentication can not found") { }  
    }
}
