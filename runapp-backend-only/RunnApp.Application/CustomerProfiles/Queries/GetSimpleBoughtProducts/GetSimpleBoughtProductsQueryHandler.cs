using ErrorOr;
using MediatR;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.CustomerProfiles.Queries.GetSimpleBoughtProducts
{
    public class GetSimpleBoughtProductsQueryHandler(ICustomerProfileRepository customerProfileRepository) : IRequestHandler<GetSimpleBoughtProductsQuery, ErrorOr<BoughtProductsDto>>
    {
        private readonly ICustomerProfileRepository _customerProfileRepository = customerProfileRepository;
        public async Task<ErrorOr<BoughtProductsDto>> Handle(GetSimpleBoughtProductsQuery request, CancellationToken cancellationToken)
        {
            var user = await _customerProfileRepository.GetCustomerProfile(request.UserId);
            if (user == null) return Error.NotFound(code: "UserProfileWasNotFoundWithGivenId", description: "User profile was not found with given id");

            return new BoughtProductsDto(user.BoughtProducts.Select(x => x).ToArray(), user.Reviews.Select(x => x).ToArray());
        }
    }
}
