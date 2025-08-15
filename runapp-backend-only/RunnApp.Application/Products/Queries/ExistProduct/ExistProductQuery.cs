using ErrorOr;
using MediatR;

namespace RunnApp.Application.Products.Queries.ExistProduct
{
    public record ExistProductQuery(Guid ProductId) : IRequest<ErrorOr<Success>>;
}
