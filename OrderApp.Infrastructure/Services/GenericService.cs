using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderApp.Domain.Concrete.Entities;
using OrderApp.Domain.Constants;
using OrderApp.Infrastructure.Exceptions;
using OrderApp.Repository.DTOs.RequestDTOs;
using OrderApp.Repository.DTOs.ResponseDTOs;
using OrderApp.Repository.Repositories;
using OrderApp.Repository.Services;
using OrderApp.Repository.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp.Infrastructure.Services
{
    public class GenericService<T, TDto> : IGenericService<T, TDto> where T : class
    {
        private readonly IGenericRepository<T> _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFilterService<T> _filterService;
        public GenericService(IGenericRepository<T> repository, IFilterService<T> filterService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _filterService = filterService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public virtual async Task<ApiResponseDto<TDto>> AddAsync(TDto entity)
        {
            var mapped = _mapper.Map<T>(entity);
            await _repository.AddAsync(mapped);
            await _unitOfWork.CommitAsync();
            return ApiResponseDto<TDto>.Success(entity);

        }

        public virtual async Task<ApiResponseDto<List<TDto>>> AddRangeAsync(List<TDto> entities)
        {
            var mapped = _mapper.Map<List<T>>(entities);
            await _repository.AddRangeAsync(mapped);
            await _unitOfWork.CommitAsync();
            return ApiResponseDto<List<TDto>>.Success(entities);
        }

        public virtual async Task<ApiResponseDto<TDto>> AnyAsync(Expression<Func<T, bool>> expression)
        {
            var entity = await _repository.AnyAsync(expression);

            if (!entity)
            {
                throw new NotFoundException(MagicStrings.NotFoundMessage<T>());
            }

            var mapped = _mapper.Map<TDto>(entity);
            return ApiResponseDto<TDto>.Success(mapped);
        }

        public virtual async Task<ApiResponseDto<List<TDto>>> GetAllAsync(List<FilterDTO> filters)
        {
            var entities = await _repository.GetAll().ToListAsync();

            if (entities == null)
            {
                throw new NotFoundException(MagicStrings.NotFoundMessage<T>());
            }

            var data = _filterService.GetFilteredData(entities, filters, out FilterResultDto filterResult);

            var mapped = _mapper.Map<List<TDto>>(data);

            return ApiResponseDto<List<TDto>>.Success(mapped, filterResult);
        }

        public virtual async Task<ApiResponseDto<TDto>> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new NotFoundException(MagicStrings.NotFoundMessage<T>());
            }
            var mapped = _mapper.Map<TDto>(entity);

            return ApiResponseDto<TDto>.Success(mapped);
        }

        public virtual async Task<ApiResponseDto<TDto>> RemoveAsync(int id)
        {
            var remove = await _repository.GetByIdAsync(id);

            if (remove == null)
            {
                throw new NotFoundException(MagicStrings.NotFoundMessage<T>());
            }

            var entity = await _repository.GetByIdAsync(id);
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
            var mapped = _mapper.Map<TDto>(entity);
            return ApiResponseDto<TDto>.Success(mapped);
        }

        public virtual async Task<ApiResponseDto<List<TDto>>> RemoveRangeAsync(List<TDto> entities)
        {
            try
            {
                var mapped = _mapper.Map<IEnumerable<T>>(entities);
                _repository.RemoveRange(mapped);
                await _unitOfWork.CommitAsync();
                return ApiResponseDto<List<TDto>>.Success(entities);

            }
            catch
            {
                throw new NotFoundException(MagicStrings.NotFoundMessage<T>());
            }
        }

        public virtual async Task<ApiResponseDto<TDto>> UpdateAsync(TDto entity)
        {
            try
            {
                var mapped = _mapper.Map<T>(entity);
                _repository.Update(mapped);
                await _unitOfWork.CommitAsync();
                var mappedDto = _mapper.Map<TDto>(entity);
                return ApiResponseDto<TDto>.Success(mappedDto);
            }
            catch
            {
                throw new NotFoundException(MagicStrings.NotFoundMessage<T>());
            }
        }

        public virtual async Task<ApiResponseDto<List<TDto>>> Where(Expression<Func<T, bool>> expression)
        {
            var entities = await _repository.Where(expression).ToListAsync();

            if (entities == null)
            {
                throw new NotFoundException(MagicStrings.NotFoundMessage<T>());
            }
            var mapped = _mapper.Map<List<TDto>>(entities);
            return ApiResponseDto<List<TDto>>.Success(mapped);
        }
    }
}
