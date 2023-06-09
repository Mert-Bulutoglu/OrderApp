﻿using OrderApp.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp.Domain.Concrete.Entities
{
    public class Order : IBaseEntity
    {
        public int Id { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerGSM { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
