namespace Contracts.Products.Responses
{
    public record CreatedProductResponseDto(Guid ProductId, string? Image, decimal Price, string Name, decimal? DiscountedPrice, string[] Category);
}
