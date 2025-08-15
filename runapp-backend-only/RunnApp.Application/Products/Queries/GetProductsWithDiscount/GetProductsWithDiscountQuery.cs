using MediatR;
using RunnApp.Application.Products.Queries.GetProducts;

namespace RunnApp.Application.Products.Queries.GetProductsWithDiscount
{
    public record GetProductsWithDiscountQuery() : IRequest<IEnumerable<ProductWithMainImage>>;
}
