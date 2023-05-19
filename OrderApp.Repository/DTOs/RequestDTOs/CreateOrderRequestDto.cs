using OrderApp.Domain.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp.Repository.DTOs.RequestDTOs
{
    public class CreateOrderRequestDto
    {
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerGSM { get; set; }
        public List<ProductDetail>? ProductDetails { get; set; }

    }
}
