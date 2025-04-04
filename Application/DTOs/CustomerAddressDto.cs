using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CustomerAddressDto
    {
        public string AddressLine { get; set; } = null!;
        public string City { get; set; } = null!;
        public string? UnitNumber { get; set; }
        public string? StreetNumber { get; set; }
    }
}
