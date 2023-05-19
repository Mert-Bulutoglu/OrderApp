using Microsoft.AspNetCore.Mvc;
using OrderApp.Repository.DTOs.RequestDTOs;
using OrderApp.Repository.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp.Repository.Services
{
    public interface IFilterService<T>
    {
        public IEnumerable<T> GetFilteredData(IEnumerable<T> data, IEnumerable<FilterDTO> filters, out FilterResultDto filterResult);
    }
}
