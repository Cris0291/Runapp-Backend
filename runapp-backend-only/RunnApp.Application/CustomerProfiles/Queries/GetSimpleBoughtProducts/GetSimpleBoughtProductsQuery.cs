using ErrorOr;
using MediatR;

namespace RunnApp.Application.CustomerProfiles.Queries.GetSimpleBoughtProducts
{
    public record GetSimpleBoughtProductsQuery(Guid UserId) : IRequest<ErrorOr<BoughtProductsDto>>;
}
