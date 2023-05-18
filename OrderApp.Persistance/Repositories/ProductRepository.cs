using OrderApp.Domain.Concrete.Entities;
using OrderApp.Persistance.Context;
using OrderApp.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp.Persistance.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}
