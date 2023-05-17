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
    internal class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            var seedData = GenerateSeedData();

            builder.HasData(seedData.ToArray());
        }

        private static List<Product> GenerateSeedData()
        {
            var seedData = new List<Product>();
            var random = new Random();

            var categories = new List<string> { "Electronics", "Clothing", "Home Decor", "Books", "Beauty", "Sports" };
            var units = new List<string> { "Piece", "Set", "Pair", "Box", "Meter", "Gram" };
            var statuses = new List<string> { "Active", "Inactive" };

            for (int i = 1; i <= 1000; i++)
            {
                var product = new Product
                {
                    Id = i,
                    Description = $"Product {i} Description",
                    Category = categories[random.Next(categories.Count)],
                    Unit = units[random.Next(units.Count)],
                    UnitPrice = random.Next(10, 1000),
                    Status = statuses[random.Next(statuses.Count)]
                };

                seedData.Add(product);
            }

            return seedData;
        }
    }
}
