using ErrorOr;
using MediatR;
using RunApp.Domain.CustomerProfileAggregate;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.CustomerProfiles.Commands.UpdateAccountInfo
{
    public class UpdateAccountInfoCommandHandler(ICustomerProfileRepository customerProfileRepository, IUnitOfWorkPattern unitOfWorkPattern) : IRequestHandler<UpdateAccountInfoCommand, ErrorOr<CustomerProfile>>
    {
        private readonly ICustomerProfileRepository _customerProfileRepository = customerProfileRepository;
        private readonly IUnitOfWorkPattern _unitOfWorkPattern = unitOfWorkPattern;
        public async Task<ErrorOr<CustomerProfile>> Handle(UpdateAccountInfoCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerProfileRepository.GetCustomerProfile(request.UserId);
            if (customer == null) return Error.NotFound(code: "UserProfileWasNotFoundWithGivenId", description: "User profile was not found with given id");

            customer.UpdateAccountInfo(request.Name, request.Email, request.NickName);

            await _unitOfWorkPattern.CommitChangesAsync();
            return customer;
        }
    }
}
