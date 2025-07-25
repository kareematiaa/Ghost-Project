using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class AdminProductCardDto
    {
        public Guid ProductId { get; set; }      
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public List<string> Colors { get; set; } = new();
    }
}
