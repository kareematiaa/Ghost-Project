using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class OrderItemDto
    {
        public Guid ProductVariantId { get; set; }
        public int Quantity { get; set; }

    }
}
