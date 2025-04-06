using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
   public class ProductVariantAdminDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public Guid ColorId { get; set; }
        public string ColorName { get; set; }
        public string ColorHex { get; set; }
        public List<ProductVariantSizeDto> AvailableSizes { get; set; }
    }
}
