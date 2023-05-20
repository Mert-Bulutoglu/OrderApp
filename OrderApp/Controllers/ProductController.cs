using Autofac.Core;
using Microsoft.AspNetCore.Mvc;
using OrderApp.Repository.DTOs.RequestDTOs;
using OrderApp.Repository.Services;

namespace OrderApp.API.Controllers
{
    public class ProductController : CustomBaseController
    {
        private readonly IProductService _service;

        public ProductController(IProductService productService)
        {
            _service = productService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetProducts(List<FilterDTO> Filters)
        {
            return Ok(await _service.GetAllAsync(Filters));
        }

    }
}
