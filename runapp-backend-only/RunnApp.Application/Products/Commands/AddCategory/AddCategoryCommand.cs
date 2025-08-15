using ErrorOr;
using MediatR;
using RunApp.Domain.ProductAggregate.Categories;

namespace RunnApp.Application.Products.Commands.AddCategory
{
    public record AddCategoryCommand(Guid ProductId, string CategoryName) : IRequest<ErrorOr<Category>>;
}
