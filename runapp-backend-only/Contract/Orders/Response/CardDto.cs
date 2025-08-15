namespace Contracts.Orders.Response
{
    public record CardDto(string CardName, string CardNumber, string Cvv, string ExpiryDate);
}
