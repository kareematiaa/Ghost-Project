using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class SettingsNotFoundException : Exception
    {
        public SettingsNotFoundException()
        {
        }

        public SettingsNotFoundException(string messege) :
            base(messege)
        { }
       
    }
}
