using ErrorOr;
using MediatR;
using RunnApp.Application.Common.SortingPagingFiltering;

namespace RunnApp.Application.Products.Queries.GetProducts
{
    public record GetProductsQuery(Guid UserId, OrderByOptions OrderByOptions, int[]? Stars, string[]? Categories, int[] PriceRange, string Search) : IRequest<ErrorOr<IEnumerable<ProductsJoin>>>;
    
    
}
