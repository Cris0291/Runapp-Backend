namespace RunnApp.Application.Orders.Commands.CreateOrder
{
    public record OrderCard(string CardName, string CardNumber, string CVV, string ExpiryDate);
}
