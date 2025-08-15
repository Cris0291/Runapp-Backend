using ErrorOr;
using MediatR;
using RunApp.Domain.CustomerProfileAggregate;

namespace RunnApp.Application.CustomerProfiles.Commands.UpdateAccountInfo
{
    public record UpdateAccountInfoCommand(Guid UserId, string Name, string Email, string NickName) : IRequest<ErrorOr<CustomerProfile>>;
}
