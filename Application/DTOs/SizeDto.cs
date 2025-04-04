using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class SizeDto
    {
        public Guid SizeId { get; set; }
        public string Name { get; set; } = null!;
        public int QuantityAvailable { get; set; }
    }
}
