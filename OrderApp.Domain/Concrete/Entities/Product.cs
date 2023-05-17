using OrderApp.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp.Domain.Concrete.Entities
{
    public class Product : IBaseEntity
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? Unit { get; set; }
        public decimal UnitPrice { get; set; }
        public string? Status { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; }

        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
