using FluentValidation;

namespace RunnApp.Application.Products.Commands.AddCategory
{
    public class AddCategoryCommandValidator : AbstractValidator<AddCategoryCommand>
    {
        public AddCategoryCommandValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty().NotNull();
        }
    }
}
