using FluentValidation;

namespace RunnApp.Application.Stocks.Commands.RemoveStock
{
    public class RemoveStockCommandValidator : AbstractValidator<RemoveStockCommand>
    {
        public RemoveStockCommandValidator()
        {
            RuleFor(x => x.RemoveStock).GreaterThan(0);
            RuleFor(x => x.ProductId).Must(x => x != Guid.Empty).WithMessage("Product id must not be empty");
            RuleFor(x => x.StoreOwnerId).Must(x => x != Guid.Empty).WithMessage("Store Profile id must not be empty");
        }
    }
}
