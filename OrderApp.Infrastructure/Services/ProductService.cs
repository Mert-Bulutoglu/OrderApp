using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderApp.Domain.Concrete.Entities;
using OrderApp.Domain.Constants;
using OrderApp.Persistance.Repositories;
using OrderApp.Repository.Cache;
using OrderApp.Repository.DTOs.EntityDtos;
using OrderApp.Repository.DTOs.RequestDTOs;
using OrderApp.Repository.DTOs.ResponseDTOs;
using OrderApp.Repository.Repositories;
using OrderApp.Repository.Services;
using OrderApp.Repository.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp.Infrastructure.Services
{
    public class ProductService : GenericService<Product, ProductDto>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFilterService<Product> _filterService;
        private readonly ICacheService<Product> _cacheService;

        public ProductService(IGenericRepository<Product> repository, ICacheService<Product> cacheService, IProductRepository productRepository, IFilterService<Product> filterService, IUnitOfWork unitOfWork, IMapper mapper) : base(repository, filterService, unitOfWork, mapper)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _filterService = filterService;
            _cacheService = cacheService;
        }

        public override async Task<ApiResponseDto<List<ProductDto>>> GetAllAsync(List<FilterDTO> filters)
        {
            List<Product> products;

            if (!_cacheService.Exists(MagicStrings.ProductsCacheKey))
            {

                products = await _productRepository.GetAll().ToListAsync();
                var productDtos = _mapper.Map<List<ProductDto>>(products);
                _cacheService.Set(MagicStrings.ProductsCacheKey, productDtos, DateTimeOffset.UtcNow.AddMinutes(3));
            }

            products = _cacheService.Get<List<Product>>(MagicStrings.ProductsCacheKey);

            var filtereddata = _filterService.GetFilteredData(products, filters, out FilterResultDto filterResult);
            var mapped = _mapper.Map<List<ProductDto>>(filtereddata);

            return ApiResponseDto<List<ProductDto>>.Success( mapped, filterResult);
        }
    }
}
