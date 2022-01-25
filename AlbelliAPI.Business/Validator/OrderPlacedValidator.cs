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

    public class ProductValidator : AbstractValidator<ProductPlaced>
    {
        public ProductValidator()
        {
            RuleFor(c => c.ProductType)
                .LessThanOrEqualTo(5).WithMessage("ProductType is invalid")
                .NotNull().NotEmpty().WithMessage("ProductType is required");
            RuleFor(c => c.Quantity).NotNull().NotEmpty().WithMessage("Quantity is required");
        }
    }
}