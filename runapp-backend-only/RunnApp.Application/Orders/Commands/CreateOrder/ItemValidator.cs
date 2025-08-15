using FluentValidation;

namespace RunnApp.Application.Orders.Commands.CreateOrder
{
    public class ItemValidator : AbstractValidator<Item>
    {
        public ItemValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.ProductName).NotNull().NotEmpty();
            RuleFor(x => x.Quantity).GreaterThan(0);
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.PriceWithDiscount).GreaterThan(0).When(x => x.PriceWithDiscount.HasValue);
        }
    }
}
