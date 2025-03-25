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
    public class ShippingCostConfiguration : IEntityTypeConfiguration<ShippingCost>
    {
        public void Configure(EntityTypeBuilder<ShippingCost> builder)
        {
            builder.HasKey(sc => new { sc.ShippingMethodId, sc.Governate });

          
        }
    }
}
