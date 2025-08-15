using FluentValidation;

namespace RunnApp.Application.Stocks.Commands.AddStock
{
    public class AddStockCommandValidator : AbstractValidator<AddStockCommand>
    {
        public AddStockCommandValidator()
        {
            RuleFor(x => x.AddedStock).GreaterThan(0);
            RuleFor(x => x.ProductId).Must(x => x != Guid.Empty).WithMessage("Product id must not be empty");
        }
    }
}
