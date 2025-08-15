using ErrorOr;
using MediatR;
using RunApp.Domain.CustomerProfileAggregate;

namespace RunnApp.Application.CustomerProfiles.Queries.GetUserAccountInfo
{
    public record GetUserAccountInfoQuery(Guid UserId) : IRequest<ErrorOr<CustomerProfile>>;
}
