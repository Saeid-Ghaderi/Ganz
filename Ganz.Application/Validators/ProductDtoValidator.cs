using FluentValidation;
using Ganz.Application.Dtos;

namespace Ganz.Application.Validators
{
    public class ProductDtoValidator:AbstractValidator<ProductRequestDTO>
    {
        public ProductDtoValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

        }
    }
}
