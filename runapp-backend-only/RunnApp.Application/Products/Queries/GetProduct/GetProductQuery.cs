using MediatR;
using ErrorOr;

namespace RunnApp.Application.Products.Queries.GetProduct
{
    public record GetProductQuery(Guid ProductId, Guid UserId) : IRequest<ErrorOr<ProductItemDto>>;


}
