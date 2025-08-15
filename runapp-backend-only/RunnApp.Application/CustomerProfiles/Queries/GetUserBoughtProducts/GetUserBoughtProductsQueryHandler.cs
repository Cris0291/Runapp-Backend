using ErrorOr;
using MediatR;
using RunApp.Domain.Products;
using RunnApp.Application.Common.Interfaces;
using RunnApp.Application.CustomerProfiles.Queries.GetUserReviews;
using RunnApp.Application.Products.Queries.GetProducts;

namespace RunnApp.Application.CustomerProfiles.Queries.GetUserBoughtProducts
{
    public class GetUserBoughtProductsQueryHandler(ICustomerProfileRepository profileRepository, IProductsRepository productsRepository, ILeftJoinRepository leftJoinRepository) : IRequestHandler<GetUserBoughtProductsQuery, ErrorOr<IEnumerable<Product>>>
    {
        private readonly ICustomerProfileRepository _profileRepository = profileRepository;
        private readonly IProductsRepository _productsRepository = productsRepository;
        private readonly ILeftJoinRepository _leftJoinRepository = leftJoinRepository;
        public async Task<ErrorOr<IEnumerable<Product>>> Handle(GetUserBoughtProductsQuery request, CancellationToken cancellationToken)
        {
            var customer = await _profileRepository.GetCustomerProfile(request.userId);
            if (customer == null) throw new InvalidOperationException("User was not found. Something unexpected happened");

            if (customer.BoughtProducts.Count == 0) return new List<Product>();

            var products = _productsRepository.GetBoughtProducts(customer.BoughtProducts);
            return await _leftJoinRepository.ExecuteQuery(products);
        }
    }
}
