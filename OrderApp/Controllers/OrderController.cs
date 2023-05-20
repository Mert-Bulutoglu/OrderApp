using Autofac.Core;
using Microsoft.AspNetCore.Mvc;
using OrderApp.Repository.DTOs.RequestDTOs;
using OrderApp.Repository.Services;

namespace OrderApp.API.Controllers
{
    public class OrderController : CustomBaseController
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService orderService)
        {
            _service = orderService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateOrder(CreateOrderRequestDto createOrderRequestDto)
        {
            return Ok(await _service.AddAsync(createOrderRequestDto));
        }
    }
}
