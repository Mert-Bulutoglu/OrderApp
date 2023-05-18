using FluentValidation;
using OrderApp.Repository.DTOs.EntityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp.Infrastructure.Validations
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Length(3, 500).WithMessage("{PropertyName} length must be between {MinLength} and {MaxLength}")
                .Matches("^[a-zA-Z0-9_]*$").WithMessage("{PropertyName} only includes alphanumeric characters.");

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Length(3, 28).WithMessage("{PropertyName} length must be between {MinLength} and {MaxLength}")
                .Matches("^[a-zA-Z0-9_]*$").WithMessage("{PropertyName} only includes alphanumeric characters.");

            RuleFor(x => x.Unit)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Length(3, 28).WithMessage("{PropertyName} length must be between {MinLength} and {MaxLength}")
                .Matches("^[a-zA-Z0-9_]*$").WithMessage("{PropertyName} only includes alphanumeric characters.");


        }
    }
}
