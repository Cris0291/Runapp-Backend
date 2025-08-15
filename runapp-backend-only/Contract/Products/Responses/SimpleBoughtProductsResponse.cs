namespace Contracts.Products.Responses
{
    public record SimpleBoughtProductsResponse(Guid[] BoughtProducts, Guid[] BoughtProductsWithReviews);
}
