using OrderApp.Repository.DTOs.RequestDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp.Repository.DTOs.ResponseDTOs
{
    public class FilterResultDto
    {
        public int TotalCount { get; set; }
        public List<FilterDTO>? Filters { get; set; }
    }
}
