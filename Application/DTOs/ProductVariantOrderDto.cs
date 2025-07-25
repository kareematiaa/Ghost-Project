using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ProductVariantOrderDto
    {
       
            public Guid Id { get; set; }
            public string ProductName { get; set; } = null!;
            public string ProductDescription { get; set; } = null!;
            public decimal Price { get; set; }
            public string ColorName { get; set; } = null!;
            public string SizeName { get; set; } = null!;
            public int Quantity { get; set; }
            public List<string> ImageUrls { get; set; } = new List<string>();
        
    }
}
