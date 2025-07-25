﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IPayementService
    {
        Task<string> CreatePaymentUrlAsync(Order order);
    }
}
