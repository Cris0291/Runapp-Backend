using ErrorOr;
using MediatR;

namespace RunnApp.Application.Products.Commands.DeleteCategory
{
    public record DeleteCategoryCommand(Guid ProductId, Guid CategoryId) : IRequest<ErrorOr<Success>>;
}
