﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp.RabbitMQ.Configurations
{
    public class RabbitMQConfiguration
    {
        public string? HostName { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
