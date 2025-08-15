using ErrorOr;
using MediatR;
using RunApp.Domain.StoreOwnerProfileAggregate;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.StoreOwnerProfiles.Commands.LoginStoreOwnerProfile
{
    public class LoginStoreOwnerProfileCommandHandler(IStoreOwnerProfileRepository profileRepository) : IRequestHandler<LoginStoreOwnerProfielCommand, ErrorOr<StoreOwnerProfile>>
    {
        private readonly IStoreOwnerProfileRepository _profileRepository = profileRepository;

        public async Task<ErrorOr<StoreOwnerProfile>> Handle(LoginStoreOwnerProfielCommand request, CancellationToken cancellationToken)
        {
            var storeOwner = await _profileRepository.GetStoreOwnerProfile(request.UserId);
            if (storeOwner == null) return Error.NotFound(code: "SalesProfileWasNotFound", description: "Sales profile was not found for the given user");

            return storeOwner;
        }
    }
}
