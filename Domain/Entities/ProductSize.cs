using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductSize :BaseEntity
    {
        public string Size { get; set; } = null!;
        public ICollection<ProductVariant> ProductVariants { get; set; } = null!;
    }
}

