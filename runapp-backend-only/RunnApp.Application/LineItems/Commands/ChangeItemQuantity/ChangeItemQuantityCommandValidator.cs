using FluentValidation;

namespace RunnApp.Application.LineItems.Commands.ChangeItemQuantity
{
    public class ChangeItemQuantityCommandValidator : AbstractValidator<ChangeItemQuantityCommand>
    {
        public ChangeItemQuantityCommandValidator()
        {
            RuleFor(x => x.Quantity).GreaterThan(0);
        }
    }
}
