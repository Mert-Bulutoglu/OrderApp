using OrderApp.Domain.Concrete.Entities;
using OrderApp.Repository.DTOs.EntityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp.Repository.Services
{
    public interface IProductService : IGenericService<Product, ProductDto>
    {
    }
}
