using FluentValidation;

namespace RunnApp.Application.LineItems.Commands.AddItem
{
    public class AddItemCommandValidator : AbstractValidator<AddItemCommand>
    {
        public AddItemCommandValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.OrderId).NotEmpty();
            RuleFor(x => x.ProductName).NotNull().NotEmpty();
            RuleFor(x => x.Quantity).GreaterThan(0);
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.PriceWithDiscount).GreaterThan(0).When(x => x.PriceWithDiscount.HasValue);
        }
    }
}
