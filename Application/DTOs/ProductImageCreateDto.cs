using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ProductImageCreateDto
    {
        public Guid ProductVariantId { get; set; }
        public string Base64Image { get; set; } = null!;
    }
}
