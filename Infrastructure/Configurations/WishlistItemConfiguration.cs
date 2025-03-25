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
    public class WishlistItemConfiguration : IEntityTypeConfiguration<WishlistItem>
    {
        public void Configure(EntityTypeBuilder<WishlistItem> builder)
        {

            builder.HasKey(cl => new { cl.ProductId, cl.WishlistId });

            builder.HasOne(wi => wi.Wishlist)
                   .WithMany(w => w.WishlistItems)
                   .HasForeignKey(wi => wi.WishlistId);


            builder.HasOne(wi => wi.Product)
                .WithMany(v => v.WishListItems)
                .HasForeignKey(wi => wi.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
                
        }
    }
}
