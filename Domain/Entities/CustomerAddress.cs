using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CustomerAddress 
    {
        public string AddressLine { get; set; } = null!;
        public string City { get; set; } = null!;
        public string? UnitNumber { get; set; }
        public string? StreetNumber { get; set; }
        public string? NearestLandMark { get; set; }
        public string? PostalCode { get; set; }
    }
}
