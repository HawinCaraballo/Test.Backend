
namespace Test.Backend.Application.Features.Products.Commands.CreateProduct
{
    using FluentValidation;
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator() 
        {
            RuleFor(p => p.Name)
                .NotNull().WithMessage("Name cannot be null");

            RuleFor(p => p.Description)
                .NotNull().WithMessage("Description cannot be null");

            RuleFor(p => p.Stock)
                .NotNull().WithMessage("Stock cannot be null");

            RuleFor(p => p.Price)
                .NotNull().WithMessage("Price cannot be null");

            RuleFor(p => p.Price)
                .NotNull().WithMessage("Price cannot be null");
        }
    }
}
