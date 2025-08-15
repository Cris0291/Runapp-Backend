using Contracts.LineItems.Response;

namespace Contracts.Orders.Response
{
    public record OrderDto(Guid OrderId, AddressDto? AddressRequest, CardDto? CardRequest);
}
