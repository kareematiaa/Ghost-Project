using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class OrderResultDto
    {
        public bool Success { get; set; }
        public Guid? OrderId { get; set; }
        public string ErrorMessage { get; set; }
    }
}
