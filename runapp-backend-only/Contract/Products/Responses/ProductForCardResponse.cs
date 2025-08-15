using Contracts.ProductStatuses.Response;

namespace Contracts.Products.Responses
{
    public record ProductForCardResponse(Guid ProductId, string Name, decimal ActualPrice, int? NumberOfReviews, double? AverageRatings, 
                                         decimal? PriceWithDiscount, string? PromotionalText, decimal? Discount,ProductStatusResponse? ProductStatusResponse);
}
