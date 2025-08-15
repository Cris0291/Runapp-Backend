namespace Contracts.Orders.Request
{
    public record ItemResquestDto(Guid Id, string Name, int Quantity, decimal Price, decimal? PriceWithDiscount, decimal TotalPrice);
}
