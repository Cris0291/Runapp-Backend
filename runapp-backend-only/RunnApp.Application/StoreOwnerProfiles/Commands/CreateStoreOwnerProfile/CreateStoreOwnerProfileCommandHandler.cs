using ErrorOr;
using MediatR;
using RunApp.Domain.StoreOwnerProfileAggregate;
using RunnApp.Application.Common.Interfaces;


namespace RunnApp.Application.StoreOwnerProfiles.Commands.CreateStoreOwnerProfile
{
    public class CreateStoreOwnerProfileCommandHandler(IStoreOwnerProfileRepository storeOwnerProfileRepository, IUnitOfWorkPattern unitOfWorkPattern) : IRequestHandler<CreateStoreOwnerProfileCommand, ErrorOr<StoreOwnerProfile>>
    {
        private readonly IStoreOwnerProfileRepository _storeOwnerProfileRepository = storeOwnerProfileRepository;
        private readonly IUnitOfWorkPattern _unitOfWorkPattern = unitOfWorkPattern;
        
        public async Task<ErrorOr<StoreOwnerProfile>> Handle(CreateStoreOwnerProfileCommand request, CancellationToken cancellationToken)
        {
            bool isStoreProfile = await _storeOwnerProfileRepository.ExistStoreOwnerProfile(request.userId);
            if (isStoreProfile) return Error.Validation(code: "StoreOwnerProfileAlreadyExistForTheGivenUser", description: "Current user already has a sales profile. Please log in as a vendor");

            var profile = StoreOwnerProfile.CreateProfile(request.userId, request.StoreName, request.AddresCommand.ZipCode,
                request.AddresCommand.Street, request.AddresCommand.City, request.AddresCommand.BuildingNumber,
                request.AddresCommand.Country, request.AddresCommand.AlternativeStreet, request.AddresCommand.AlternativeBuildingNumber.Value,
                request.CardCommand.HoldersName, request.CardCommand.CardNumber, request.CardCommand.CVV, request.CardCommand.ExpiryDate, request.InitailInvestment);

            if (profile.IsError) return profile.Errors;

            await _storeOwnerProfileRepository.CreateStoreOwnerProfile(profile.Value);
            int wasAdded = await _unitOfWorkPattern.CommitChangesAsync();
            if (wasAdded == 0) throw new InvalidOperationException("Profile could not be added to the database");
            return profile.Value;
        }
    }
}
