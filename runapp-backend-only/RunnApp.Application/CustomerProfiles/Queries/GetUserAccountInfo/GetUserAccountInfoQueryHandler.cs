using ErrorOr;
using MediatR;
using RunApp.Domain.CustomerProfileAggregate;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.CustomerProfiles.Queries.GetUserAccountInfo
{
    public class GetUserAccountInfoQueryHandler(ICustomerProfileRepository customerProfileRepository) : IRequestHandler<GetUserAccountInfoQuery, ErrorOr<CustomerProfile>>
    {
        private readonly ICustomerProfileRepository _customerProfileRepository = customerProfileRepository;
        public async Task<ErrorOr<CustomerProfile>> Handle(GetUserAccountInfoQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerProfileRepository.GetCustomerProfile(request.UserId);
            if (customer == null) return Error.NotFound(code: "UserProfileWasNotFoundWithGivenId", description: "User profile was not found with given id");

            return customer;
        }
    }
}
