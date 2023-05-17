using OrderApp.Repository.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp.Repository.Services
{
    public interface IGenericService<T, TDto> where T : class
    {
        public Task<ApiResponseDto<TDto>> GetByIdAsync(int id);
        public Task<ApiResponseDto<TDto>> GetAllAsync();
        public Task<ApiResponseDto<List<TDto>>> Where(Expression<Func<T, bool>> expression);
        public Task<ApiResponseDto<TDto>> AnyAsync(Expression<Func<T, bool>> expression);
        public Task<ApiResponseDto<TDto>> AddAsync(TDto entity);
        public Task<ApiResponseDto<List<TDto>>> AddRangeAsync(List<TDto> entities);
        public Task<ApiResponseDto<TDto>> UpdateAsync(TDto entity);
        public Task<ApiResponseDto<TDto>> RemoveAsync(int id);
        public Task<ApiResponseDto<List<TDto>>> RemoveRangeAsync(List<TDto> entities);
    }
}
