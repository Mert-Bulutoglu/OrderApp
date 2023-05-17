using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderApp.Domain.Constants;
using OrderApp.Infrastructure.Exceptions;
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
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GenericService(IGenericRepository<T> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponseDto<T>> AddAsync(T entity)
        {
            var mapped = _mapper.Map<T>(entity);
            await _repository.AddAsync(mapped);
            await _unitOfWork.CommitAsync();
            return ApiResponseDto<T>.Success(entity);

        }

        public async Task<ApiResponseDto<List<T>>> AddRangeAsync(List<T> entities)
        {
            var mapped = _mapper.Map<List<T>>(entities);
            await _repository.AddRangeAsync(mapped);
            await _unitOfWork.CommitAsync();
            return ApiResponseDto<List<T>>.Success(entities);
        }

        public async Task<ApiResponseDto<T>> AnyAsync(Expression<Func<T, bool>> expression)
        {
            var entity = await _repository.AnyAsync(expression);

            if (!entity)
            {
                throw new NotFoundException(MagicStrings.NotFoundMessage<T>());
            }

            var mapped = _mapper.Map<T>(entity);
            return ApiResponseDto<T>.Success(mapped);
        }

        public async Task<ApiResponseDto<List<T>>> GetAllAsync()
        {
            var entities = await _repository.GetAll().ToListAsync();

            if (entities == null)
            {
                throw new NotFoundException(MagicStrings.NotFoundMessage<T>());
            }

            var mapped = _mapper.Map<List<T>>(entities);

            return ApiResponseDto<List<T>>.Success(mapped);
        }

        public async Task<ApiResponseDto<T>> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new NotFoundException(MagicStrings.NotFoundMessage<T>());
            }
            var mapped = _mapper.Map<T>(entity);

            return ApiResponseDto<T>.Success(mapped);
        }

        public async Task<ApiResponseDto<T>> RemoveAsync(int id)
        {
            var remove = await _repository.GetByIdAsync(id);

            if (remove == null)
            {
                throw new NotFoundException(MagicStrings.NotFoundMessage<T>());
            }

            var entity = await _repository.GetByIdAsync(id);
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
            var mapped = _mapper.Map<T>(entity);
            return ApiResponseDto<T>.Success(mapped);
        }

        public async Task<ApiResponseDto<List<T>>> RemoveRangeAsync(List<T> entities)
        {
            try
            {
                var mapped = _mapper.Map<IEnumerable<T>>(entities);
                _repository.RemoveRange(mapped);
                await _unitOfWork.CommitAsync();
                return ApiResponseDto<List<T>>.Success(entities);

            }
            catch
            {
                throw new NotFoundException(MagicStrings.NotFoundMessage<T>());
            }
        }

        public async Task<ApiResponseDto<T>> UpdateAsync(T entity)
        {
            try
            {
                var mapped = _mapper.Map<T>(entity);
                _repository.Update(mapped);
                await _unitOfWork.CommitAsync();
                return ApiResponseDto<T>.Success(mapped);
            }
            catch
            {
                throw new NotFoundException(MagicStrings.NotFoundMessage<T>());
            }
        }

        public async Task<ApiResponseDto<List<T>>> Where(Expression<Func<T, bool>> expression)
        {
            var entities = await _repository.Where(expression).ToListAsync();

            if (entities == null)
            {
                throw new NotFoundException(MagicStrings.NotFoundMessage<T>());
            }
            var mapped = _mapper.Map<List<T>>(entities);
            return ApiResponseDto<List<T>>.Success(mapped);
        }
    }
}
