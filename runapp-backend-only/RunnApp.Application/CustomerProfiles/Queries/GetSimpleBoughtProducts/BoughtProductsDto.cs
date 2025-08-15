namespace RunnApp.Application.CustomerProfiles.Queries.GetSimpleBoughtProducts
{
    public record BoughtProductsDto(Guid[] BoughtProducts, Guid[] BoughtproductsWithReviews);
}
