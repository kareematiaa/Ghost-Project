﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users
{
    public interface IAdmin :IAppUser
    {
        public new int Id { get; set; }

    }
}
