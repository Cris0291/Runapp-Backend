using ErrorOr;
using MediatR;
using RunApp.Domain.Common.ValueType;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.CustomerProfiles.Commands.UpdateAddress
{
    public class UpdateAddressCommnadHandler(ICustomerProfileRepository customerProfileRepository, IUnitOfWorkPattern unitOfWorkPattern) : IRequestHandler<UpdateAddressCommand, ErrorOr<Address>>
    {
        private readonly ICustomerProfileRepository _customerProfileRepository = customerProfileRepository;
        private readonly IUnitOfWorkPattern _unitOfWorkPattern = unitOfWorkPattern;
        public async Task<ErrorOr<Address>> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerProfileRepository.GetCustomerProfile(request.UserId);
            if (customer == null) return Error.NotFound(code: "UserProfileWasNotFoundWithGivenId", description: "User profile was not found with given id");

            var address = customer.UpdateAddress(request.ZipCode, request.Street, request.City, request.Country, request.State);

            await _unitOfWorkPattern.CommitChangesAsync();
            return address;
        }
    }
}
