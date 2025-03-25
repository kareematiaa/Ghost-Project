﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class OrderProductVariantConfiguration : IEntityTypeConfiguration<OrderProductVariant>
    {
        public void Configure(EntityTypeBuilder<OrderProductVariant> builder)
        {
            builder.HasKey(oi => oi.Id);

            builder.HasOne(opv => opv.Order)
           .WithMany(o => o.OrderItems)
           .HasForeignKey(opv => opv.OrderId)
           .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(opv => opv.ProductVariant)
                .WithMany(pv => pv.OrderProductVariants)
                .HasForeignKey(opv => opv.ProductVariantId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
