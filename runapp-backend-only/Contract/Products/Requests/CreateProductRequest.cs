namespace Contracts.Products.Requests
{
    public record CreateProductRequest(string Name, string Description, decimal Price, ICollection<string> Bulletpoints, decimal? PriceWithDiscount, string? PromotionalText, CharacteristicsRequest Characteristics, ICollection<string> Categories);

}
