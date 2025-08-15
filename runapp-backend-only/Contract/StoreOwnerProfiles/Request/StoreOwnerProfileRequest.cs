using Contracts.Common;

namespace Contracts.StoreOwnerProfiles.Request
{
    public record StoreOwnerProfileRequest(string StoreName, AddressRequest AddressRequest, CardRequest CardRequest, decimal InitialInvestment, CustomClaim[] Claims);
}
