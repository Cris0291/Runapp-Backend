namespace RunnApp.Application.Orders.Commands.CreateOrder
{
    public record Item(Guid ProductId, string ProductName, int Quantity, decimal Price, decimal? PriceWithDiscount, decimal? Discount);
}
