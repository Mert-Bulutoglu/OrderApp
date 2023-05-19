using AutoMapper;
using OrderApp.Domain.Concrete.Entities;
using OrderApp.Repository.DTOs.EntityDTOs;
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
    public class OrderService : GenericService<Order, OrderDto>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFilterService<Order> _filterService;

        public OrderService(IGenericRepository<Order> repository, IOrderRepository orderRepository, IFilterService<Order> filterService, IUnitOfWork unitOfWork, IMapper mapper) : base(repository, filterService, unitOfWork, mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _filterService = filterService;
            _unitOfWork = unitOfWork;
        }
    }
}
