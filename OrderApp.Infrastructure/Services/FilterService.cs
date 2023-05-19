using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using OrderApp.Domain.Attributes;
using OrderApp.Domain.Constants;
using OrderApp.Repository.DTOs.RequestDTOs;
using OrderApp.Repository.DTOs.ResponseDTOs;
using OrderApp.Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp.Infrastructure.Services
{
    public class FilterService<T> : IFilterService<T>
    {
        public IEnumerable<T> GetFilteredData(IEnumerable<T> data, IEnumerable<FilterDTO> filters, out FilterResultDto filterResult)
        {
            var query = data.AsQueryable();
            List<FilterDTO> appliedfilters = new List<FilterDTO>();
            
            if (filters.Any())
            {
                Type objType = typeof(T);

                foreach (var filter in filters)
                {
                    var obj = objType.GetProperty(filter.FilterField);
                    if (obj != null && Attribute.IsDefined(obj, typeof(FilterAttribute)) == true)
                    {
                        //generate dynamic lambda expression 
                        var parameter = Expression.Parameter(typeof(T), "x");
                        var parameterOfCondition = Expression.PropertyOrField(parameter, filter.FilterField);
                        var condition = Expression.Constant(Convert.ChangeType(filter.Value, parameterOfCondition.Type));
                        Expression comparison;

                        comparison = Expression.Equal(parameterOfCondition, condition);
                        appliedfilters.Add(filter);
                                

                        var lambda = Expression.Lambda<Func<T, bool>>(comparison, parameter);

                        query = query.Where(lambda);
                    }

                }
                if (appliedfilters.Any())

                {
                    var smth = query.Count();
                    filterResult = new FilterResultDto { Filters = appliedfilters, TotalCount = smth };
                    return query.ToList();
                }



            }

            filterResult = null;

            return query.ToList();
        }
    }
}
