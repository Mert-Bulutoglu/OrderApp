﻿using OrderApp.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp.Domain.Concrete.Entities
{
    public class ProductDetail : IBaseEntity
    {
        public int Id { get; set; }
        public int Amount { get; set; }

    }
}
