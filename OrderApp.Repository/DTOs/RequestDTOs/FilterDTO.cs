using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp.Repository.DTOs.RequestDTOs
{
    public class FilterDTO
    {
        public string? FilterField { get; set; } 
        public string? Value { get; set; }
    }
}
