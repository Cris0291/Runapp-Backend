using ErrorOr;
using MediatR;
using RunnApp.Application.Common.Interfaces;

namespace RunnApp.Application.CustomerProfiles.Queries.GetUserReviews
{
    public class GetUserReviewsQueryHandler(ICustomerProfileRepository profileRepository, IProductsRepository productsRepository, ILeftJoinRepository leftJoinRepository, IReviewsRepository reviewsRepository) : IRequestHandler<GetUserReviewsQuery, ErrorOr<List<ReviewWithProductImage>>>
    {
        private readonly ICustomerProfileRepository _profileRepository = profileRepository;
        private readonly IProductsRepository _productsRepository = productsRepository;
        private readonly ILeftJoinRepository _leftJoinRepository = leftJoinRepository;
        private readonly IReviewsRepository _reviewsRepository = reviewsRepository;
        public async Task<ErrorOr<List<ReviewWithProductImage>>> Handle(GetUserReviewsQuery request, CancellationToken cancellationToken)
        {
            var customer = await _profileRepository.GetCustomerProfile(request.UserId);
            if (customer == null) throw new InvalidOperationException("User was not found. Something unexpected happened");

            if (customer.BoughtProducts.Count == 0 && customer.Reviews.Count > 0) return Error.Validation(code: "CannotReviewAProductThatHasNotBeenBought", description: "User can only review products that has bought");
            if (customer.Reviews.Count == 0) return new List<ReviewWithProductImage>();

            var products = _productsRepository.GetBoughtProducts(customer.BoughtProducts);

            var productsDto = products.FromProductsToProductsDto();
            var productsImage = await _leftJoinRepository.ExecuteQuery(productsDto);

            var userReviews = await _reviewsRepository.GetUserReviews(customer.Reviews, customer.Id);

            return userReviews.CreateReviewsWithProduct(productsImage);

        }
    }
}
