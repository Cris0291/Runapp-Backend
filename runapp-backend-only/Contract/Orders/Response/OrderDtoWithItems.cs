using Contracts.LineItems.Response;

namespace Contracts.Orders.Response
{
    public record OrderDtoWithItems(Guid OrderId, AddressDto? AddressRequest, CardDto? CardRequest, IEnumerable<LineItemDtoResponse> Items);
}
