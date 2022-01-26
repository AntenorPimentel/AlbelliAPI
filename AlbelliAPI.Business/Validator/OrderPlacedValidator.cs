using AlbelliAPI.Business.Models;
using AlbelliAPI.Data.Models;
using FluentValidation;

namespace AlbelliAPI.Business.Validator
{
    public class OrderPlacedValidator : AbstractValidator<OrderPlaced>
    {
        public OrderPlacedValidator()
        {
            RuleFor(c => c.OrderId).NotNull().NotEmpty().WithMessage("OrderID is required");
            RuleForEach(x => x.Products).SetValidator(new ProductValidator());
        }
    }

    public class ProductValidator : AbstractValidator<ProductDetails>
    {
        public ProductValidator()
        {
            RuleFor(c => c.ProductType)
                .NotNull().IsEnumName(typeof(Enums.ProductTypes), caseSensitive: false).WithMessage("ProductType is invalid");
            RuleFor(c => c.Quantity).NotNull().NotEmpty().WithMessage("Quantity is required");
        }
    }
}