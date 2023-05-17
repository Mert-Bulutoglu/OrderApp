using OrderApp.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp.Domain.Concrete.Entities
{
    public class OrderDetail : IBaseEntity
    {
        public int Id { get; set; }
        public decimal UnitPrice { get; set; }

  
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
