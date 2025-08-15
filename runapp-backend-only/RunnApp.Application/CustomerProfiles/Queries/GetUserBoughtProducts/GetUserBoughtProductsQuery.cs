using ErrorOr;
using MediatR;
using RunApp.Domain.Products;


namespace RunnApp.Application.CustomerProfiles.Queries.GetUserBoughtProducts
{
    public record GetUserBoughtProductsQuery(Guid userId) : IRequest<ErrorOr<IEnumerable<Product>>>;
}
