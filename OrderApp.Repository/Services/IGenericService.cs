using OrderApp.Repository.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp.Repository.Services
{
    public interface IGenericService<T> where T : class
    {
        public Task<ApiResponseDto<T>> GetByIdAsync(int id);
        public Task<ApiResponseDto<T>> GetAllAsync();
        public Task<ApiResponseDto<List<T>>> Where(Expression<Func<T, bool>> expression);
        public Task<ApiResponseDto<T>> AnyAsync(Expression<Func<T, bool>> expression);
        public Task<ApiResponseDto<T>> AddAsync(T entity);
        public Task<ApiResponseDto<List<T>>> AddRangeAsync(List<T> entities);
        public Task<ApiResponseDto<T>> UpdateAsync(T entity);
        public Task<ApiResponseDto<T>> RemoveAsync(int id);
        public Task<ApiResponseDto<List<T>>> RemoveRangeAsync(List<T> entities);
    }
}
