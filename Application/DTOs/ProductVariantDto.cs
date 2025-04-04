using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ProductVariantDto
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public string ColorName { get; set; } // Assuming you want to include the color name
        public List<SizeDto> AvailableSizes { get; set; } = new();
        public ICollection<ProductImageDto> ProductImages { get; set; } = null!;
    }
}
