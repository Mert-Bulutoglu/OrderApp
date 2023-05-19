using AutoMapper;
using FluentValidation;
using OrderApp.Domain.Concrete.Entities;
using OrderApp.Infrastructure.Services;
using OrderApp.Persistance.Repositories;
using OrderApp.Repository.DTOs.EntityDTOs;
using OrderApp.Repository.DTOs.RequestDTOs;
using OrderApp.Repository.DTOs.ResponseDTOs;
using OrderApp.Repository.Repositories;
using OrderApp.Repository.Services;
using OrderApp.Repository.UnitOfWorks;
using StackExchange.Redis;
using Order = OrderApp.Domain.Concrete.Entities.Order;

namespace OrderApp.Infrastructure.Services
{
    public class OrderService : GenericService<Order, CreateOrderRequestDto>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFilterService<Order> _filterService;

        public OrderService(IGenericRepository<Order> repository, IProductRepository productRepository, IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, IFilterService<Order> filterService, IUnitOfWork unitOfWork, IMapper mapper) : base(repository, filterService, unitOfWork, mapper)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _filterService = filterService;
            _unitOfWork = unitOfWork;
        }

        public override async Task<ApiResponseDto<CreateOrderRequestDto>> AddAsync(CreateOrderRequestDto entity)
        {

            var mappedOrder = _mapper.Map<Order>(entity);
            decimal TotalPrice = 0;

            await _orderRepository.AddAsync(mappedOrder);
            await _unitOfWork.CommitAsync();

            foreach (var singleProduct in entity.ProductDetails)
            {
                var product = _productRepository.GetQuery().FirstOrDefault(x => x.Id == singleProduct.Id);
                var detailPrice = singleProduct.Amount * product.UnitPrice;
                TotalPrice += detailPrice;

                var mappedOrderDetail = new OrderDetail
                {
                    OrderId = mappedOrder.Id,
                    ProductId = product.Id,
                    UnitPrice = detailPrice
                };

                await _orderDetailRepository.AddAsync(mappedOrderDetail);
                await _unitOfWork.CommitAsync();
            }

            mappedOrder.TotalAmount = TotalPrice;
            await _unitOfWork.CommitAsync();

            var responseEntity = _mapper.Map<CreateOrderRequestDto>(mappedOrder);
            return ApiResponseDto<CreateOrderRequestDto>.Success(responseEntity);
        }
    }
}
