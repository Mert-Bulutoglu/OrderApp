using FluentValidation;
using OrderApp.Repository.DTOs.EntityDtos;
using OrderApp.Repository.DTOs.RequestDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp.Infrastructure.Validations
{
    public class OrderDtoValidator : AbstractValidator<CreateOrderRequestDto>
    {
        public OrderDtoValidator()
        {
            RuleFor(x => x.CustomerName)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .Length(3, 500).WithMessage("{PropertyName} length must be between {MinLength} and {MaxLength}")
               .Matches("^[a-zA-Z0-9_]*$").WithMessage("{PropertyName} only includes alphanumeric characters.");

            RuleFor(x => x.CustomerEmail)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Length(3, 28).WithMessage("{PropertyName} length must be between {MinLength} and {MaxLength}")
                .Matches("(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])").WithMessage("Please enter valid email.");

            RuleFor(x => x.CustomerGSM)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Length(3, 21).WithMessage("{PropertyName} length must be between {MinLength} and {MaxLength}")
                .Matches("^[0-9]+$").WithMessage("{PropertyName} only includes numbers.");
            RuleFor(x => x.ProductDetails)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
