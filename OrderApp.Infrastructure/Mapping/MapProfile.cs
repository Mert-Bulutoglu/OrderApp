using AutoMapper;
using OrderApp.Domain.Concrete.Entities;
using OrderApp.Repository.DTOs.EntityDtos;
using OrderApp.Repository.DTOs.ResponseDTOs;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp.Infrastructure.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<CreateOrderRequestDto, OrderApp.Domain.Concrete.Entities.Order>();
            CreateMap<CreateOrderRequestDto, OrderDetail>();

        }
    }
}
