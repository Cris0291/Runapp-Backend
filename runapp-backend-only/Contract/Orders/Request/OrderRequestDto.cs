using Contracts.Common;

namespace Contracts.Orders.Request
{
    public record OrderRequestDto(AddressRequest? AddressRequest, CardRequest? CardRequest);
}
