using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class IdentityException : Exception
    { 
        public IdentityException(string message) :
    base(message)
        { }
    }
}
