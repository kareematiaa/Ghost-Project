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
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.OwnsOne(c => c.CustomerAddress, sa => sa.WithOwner());

            builder.HasMany(c => c.PaymentMethods)
                  .WithOne(p => (Customer)p.Customer)
                  .HasForeignKey(p => p.CustomerId);

            builder.HasOne(c => c.Cart)
                  .WithOne(ca => (Customer)ca.Customer)
                  .HasForeignKey<Cart>(ca => ca.CustomerId);


            builder.HasOne(c => c.Wishlist)
                  .WithOne(w => (Customer)w.Customer)
                  .HasForeignKey<Wishlist>(w => w.CustomerId);

        }
    }
}
