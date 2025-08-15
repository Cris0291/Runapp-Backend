namespace Contracts.Products.Requests
{
    public record GetAllProductsRequestDto(string? SortBy, string? FilterByStars, string? FilterByCategory, int[] FilterByPrice, string Search);
}
