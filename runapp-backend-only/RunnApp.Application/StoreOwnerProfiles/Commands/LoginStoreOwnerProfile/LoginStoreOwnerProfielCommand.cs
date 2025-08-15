using ErrorOr;
using MediatR;
using RunApp.Domain.StoreOwnerProfileAggregate;

namespace RunnApp.Application.StoreOwnerProfiles.Commands.LoginStoreOwnerProfile
{
    public record LoginStoreOwnerProfielCommand(Guid UserId) : IRequest<ErrorOr<StoreOwnerProfile>>;
}
