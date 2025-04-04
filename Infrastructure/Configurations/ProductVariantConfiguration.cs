using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
    {
        public void Configure(EntityTypeBuilder<ProductVariant> builder)
        {


            builder.HasKey(v => v.Id);
            builder.Property(v => v.Quantity).HasDefaultValue(0);

            builder.HasOne(v => v.Product)
                   .WithMany(p => p.ProductVariants)
                   .HasForeignKey(v => v.ProductId);


            builder.HasOne(pv => pv.ProductColor)
                .WithMany(pc => pc.ProductVariants)
                .HasForeignKey(pv => pv.ColorId);

         

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
