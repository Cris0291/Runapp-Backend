namespace Contracts.StoreOwnerProfiles.Response
{
    public record StoreOwnerProfileResponse(Guid StoreProfileId, Guid UserId, string StoreName, AddressResponse Address, string SalesLevel, bool IsAccountPaid, int TotalProductsSold, decimal TotalSalesInCash, int TotalStock, decimal InitialInvestment,string Token);
}
