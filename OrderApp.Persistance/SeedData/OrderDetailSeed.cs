using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderApp.Domain.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp.Persistance.SeedData
{
    internal class OrderDetailSeed : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            Random random = new Random();
            builder.HasData(new OrderDetail { Id = 1, UnitPrice = random.Next(1,1000) , OrderId = 1, ProductId = 1});
        }
    }
}
