using Domain.Entities;
using Domain.Users;
using Infrastructure.Context.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Date)
              .HasDefaultValueSql("GetDate()");


            builder.HasOne(o => (Customer)o.Customer)
            .WithMany(o => o.Orders)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(o => o.ShippingCost)
                .WithMany(sc => sc.Orders)
                .HasForeignKey(o => new { o.ShippingMethodId , o.Governate })
                .OnDelete(DeleteBehavior.Restrict);

            builder.OwnsOne(o => o.CustomerAddress);


            builder.HasOne(o => o.PaymentMethod)
                .WithMany(i => i.Orders)
                .HasForeignKey(o => o.PaymentMethodId);

            builder.HasMany(o => o.OrderItems)
                     .WithOne(oi => oi.Order)
                    .HasForeignKey(oi => oi.OrderId);

        }
    }
}
