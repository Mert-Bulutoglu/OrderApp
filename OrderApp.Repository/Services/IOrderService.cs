using OrderApp.Domain.Concrete.Entities;
using OrderApp.Repository.DTOs.EntityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp.Repository.Services
{
    public interface IOrderService : IGenericService<Order, OrderDto>
    {
    }
}
