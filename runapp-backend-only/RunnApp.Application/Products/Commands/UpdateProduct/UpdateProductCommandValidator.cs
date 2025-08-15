using FluentValidation;

namespace RunnApp.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(command => command.Name).NotNull().NotEmpty();
            RuleFor(command => command.Description).NotNull().NotEmpty();
            RuleFor(command => command.Price).GreaterThan(0);
            RuleFor(command => command.Brand).NotNull().NotEmpty();
            RuleFor(command => command.Type).NotNull().NotEmpty();
            RuleFor(command => command.Color).NotNull().NotEmpty();
            RuleFor(command => command.Weight).GreaterThan(0);
        }
    }
}
