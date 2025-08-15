using Contracts.Reviews.Requests;
using Contracts.Reviews.Responses;
using RunApp.Domain.ReviewAggregate;
using RunApp.Domain.ReviewAggregate.ReviewEnum;
using RunnApp.Application.CustomerProfiles.Queries.GetUserReviews;
using RunnApp.Application.Reviews.Commands.CreateReview;

namespace RunApp.Api.Mappers.Reviews
{
    public static class ReviewMapper
    {
        public static CreateReviewCommand ReviewRequestToReviewCommand(this AddReviewRequest reviewRequest, Guid productId, ReviewDescriptionEnums reviewEnum, Guid userId)
        {
            return new CreateReviewCommand(productId, userId, reviewRequest.Comment, reviewRequest.Rating, reviewEnum);
        }
        public static ReviewResponse ReviewToReviewResponse(this Review review)
        {
            return new ReviewResponse(review.Comment, review.Rating, review.Date, review.ReviewDescription.Name, review.ReviewId);
        }
        public static IEnumerable<UserReviewsResponse> ReviewsToUserReviewsResponse(this List<ReviewWithProductImage> reviews)
        {
            return reviews.Select(x => x.ReviewToUserReviewResponse());
        }
        public static UserReviewsResponse ReviewToUserReviewResponse(this ReviewWithProductImage review)
        {
            return new UserReviewsResponse(review.ProductImage?.ProductId, review.ProductImage?.Name, review.Review.ReviewId, review.Review.Rating, review.Review.ReviewDescription.Name, review.Review.Comment, review.Review.Date);
        }
    }
}
