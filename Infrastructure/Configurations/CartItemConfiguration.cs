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
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {


            builder.Ignore(ci => ci.Id);

            builder.HasKey(ci => new { ci.CartId, ci.ProductVariantId,ci.SizeId });
            builder.Property(ci => ci.Quantity).HasDefaultValue(1);

            builder.HasOne(ci => ci.ProductVariant)
                   .WithMany(pv => pv.CartItems)
                   .HasForeignKey(ci => ci.ProductVariantId);

            builder.HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId);

            builder.HasOne(ci => ci.Size)
       .WithMany()
       .HasForeignKey(ci => ci.SizeId)
       .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
