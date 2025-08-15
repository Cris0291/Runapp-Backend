namespace Contracts.LineItems.Response
{
    public record LineItemDtoResponse(Guid LineItemID, Guid ProductId, string Name, int Quantity, decimal Price, decimal? PriceWithDiscount, decimal TotalPrice);
}
