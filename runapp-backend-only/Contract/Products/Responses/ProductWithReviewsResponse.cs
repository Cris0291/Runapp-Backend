using Contracts.Reviews.Responses;

namespace Contracts.Products.Responses
{
    public record ProductWithReviewsResponse(Guid ProductId, string Name, string Description, decimal Price, IEnumerable<string> Bulletpoints, decimal? PriceWithDiscount, string? PromotionalText, decimal? Discount, int NumerOfreviews, double AverageRatings, IEnumerable<ReviewResponse> Reviews);
}
