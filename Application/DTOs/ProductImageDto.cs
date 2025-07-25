using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ProductImageDto
    {
        public string URL { get; set; } = null!;
        public Guid Id { get; set; } 
    }
}
