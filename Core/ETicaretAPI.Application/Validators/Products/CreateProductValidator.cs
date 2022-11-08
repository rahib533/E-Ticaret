using ETicaretAPI.Application.ViewModels.Products;
using FluentValidation;

namespace ETicaretAPI.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
            RuleFor(p=>p.Name).NotEmpty().NotNull().MaximumLength(100).MinimumLength(2);
            RuleFor(p=>p.Stock).GreaterThanOrEqualTo(0);
            RuleFor(p=>p.Price).GreaterThanOrEqualTo(0);
        }
    }
}