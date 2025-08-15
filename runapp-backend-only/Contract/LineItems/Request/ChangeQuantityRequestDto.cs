namespace Contracts.LineItems.Request
{
    public record ChangeQuantityRequestDto(int Quantity, Guid ProductId);
}
