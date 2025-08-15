using ErrorOr;
using MediatR;
using RunApp.Domain.StoreOwnerProfileAggregate;

namespace RunnApp.Application.StoreOwnerProfiles.Commands.CreateStoreOwnerProfile
{
    public record CreateStoreOwnerProfileCommand(Guid userId, string StoreName, AddresCommand AddresCommand, CardCommand CardCommand, decimal InitailInvestment) : IRequest<ErrorOr<StoreOwnerProfile>>;

}
