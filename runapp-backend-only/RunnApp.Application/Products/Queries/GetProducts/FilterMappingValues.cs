namespace RunnApp.Application.Products.Queries.GetProducts
{
    public record FilterMappingValues(int[]? Stars, string[]? Categories, int[] PriceRange, string Search);
}
