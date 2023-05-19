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

        [HttpPost]
        public async Task<IActionResult> All(List<FilterDTO> Filters)
        {
            return Ok(await _service.GetAllAsync(Filters));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {

            return Ok(await _service.RemoveAsync(id));
        }


    }
}
