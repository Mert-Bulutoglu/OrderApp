﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp.Domain.Abstract
{
    public interface IBaseEntity
    {
        public int Id { get; set; }
    }
}
