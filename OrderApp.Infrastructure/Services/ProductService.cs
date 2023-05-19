using AutoMapper;
using OrderApp.Domain.Concrete.Entities;
using OrderApp.Persistance.Repositories;
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

        public ProductService(IGenericRepository<Product> repository, IProductRepository productRepository, IFilterService<Product> filterService, IUnitOfWork unitOfWork, IMapper mapper) : base(repository, filterService, unitOfWork, mapper)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _filterService = filterService;
        }
    }
}
