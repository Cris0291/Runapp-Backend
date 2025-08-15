namespace Contracts.Products.Responses
{
    public record ProductsWithDiscount(Guid ProductId, string ProductName, string? Image, decimal Price, decimal? Discount, decimal? PriceWithDiscount, string Size);
}
