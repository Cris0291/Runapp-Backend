using ErrorOr;
using MediatR;
using RunApp.Domain.Common.ValueType;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.CustomerProfiles.Commands.AddAddress
{
    public class AddAddressCommandHandler(ICustomerProfileRepository profileRepository, IUnitOfWorkPattern unitOfWorkPattern) : IRequestHandler<AddAddressCommand, ErrorOr<Address>>
    {
        private readonly ICustomerProfileRepository _profileRepository = profileRepository;
        private readonly IUnitOfWorkPattern _unitOfWorkPattern = unitOfWorkPattern;
        public async Task<ErrorOr<Address>> Handle(AddAddressCommand request, CancellationToken cancellationToken)
        {
            var user = await _profileRepository.GetCustomerProfile(request.UserId);
            if (user == null) throw new InvalidOperationException("User profile was not found with given id");

            var address = user.AddAddress(request.ZipCode, request.Street, request.City, request.Country, request.State);

            await _unitOfWorkPattern.CommitChangesAsync();
            return address;
        }
    }
}
