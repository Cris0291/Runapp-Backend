namespace Contracts.Products.Responses
{
    public record UserBoughtProductsResponse(Guid ProductId, decimal ProductPrice, string Name, string Category);
}
