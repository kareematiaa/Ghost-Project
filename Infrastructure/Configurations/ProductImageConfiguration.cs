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
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {

            builder.HasKey(i => i.Id);
            builder.Property(i => i.URL).IsRequired();

            builder.HasOne(i => i.ProductVariant)
                   .WithMany(v => v.ProductImages)
                   .HasForeignKey(i => i.ProductVariantId);

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
