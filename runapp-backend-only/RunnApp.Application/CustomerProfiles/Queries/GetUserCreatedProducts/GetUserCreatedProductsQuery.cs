using ErrorOr;
using MediatR;
using RunnApp.Application.Products.Queries.GetProducts;

namespace RunnApp.Application.CustomerProfiles.Queries.GetUserCreatedProducts
{
    public record GetUserCreatedProductsQuery(Guid UserId) : IRequest<ErrorOr<IEnumerable<ProductWithMainImage>>>;
}
