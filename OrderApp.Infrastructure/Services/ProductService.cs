using AutoMapper;
using OrderApp.Domain.Concrete.Entities;
using OrderApp.Persistance.Repositories;
using OrderApp.Repository.DTOs.EntityDtos;
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
        public ProductService(IGenericRepository<Product> repository, IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper) : base(repository, unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _productRepository = productRepository;
        }
    }
}
