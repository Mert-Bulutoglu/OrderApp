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
    internal class OrderSeed : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            Random random = new Random();
            builder.HasData(new Order { Id = 1, CustomerEmail = "random@gmail.com", CustomerGSM = "52151224", CustomerName = "RandomName", TotalAmount = random.Next(1,2000)});
        }
    }
}
