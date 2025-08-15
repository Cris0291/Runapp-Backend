using Microsoft.AspNetCore.Authorization;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.Common.Authorization
{
    public class IsProductBoughtHandler(ICustomerProfileRepository customerProfileRepository) : AuthorizationHandler<ProductMusBeBoughtRequirement , AuthorizeRatingDto>
    {
        private readonly ICustomerProfileRepository _customerProfileRepository = customerProfileRepository;

        protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, ProductMusBeBoughtRequirement requirement, AuthorizeRatingDto resource)
        {
            var customer = await _customerProfileRepository.GetCustomerProfile(resource.CustomerId);
            if (customer == null) throw new InvalidOperationException("Customer profile was not found");

            bool isProductBought = customer.IsProductBought(resource.ProductId);

            if (isProductBought) context.Succeed(requirement);
        }
    }
}
