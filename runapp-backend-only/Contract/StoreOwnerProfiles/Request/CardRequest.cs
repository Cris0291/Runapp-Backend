namespace Contracts.StoreOwnerProfiles.Request
{
    public record CardRequest(string HoldersName, int CardNumber, int CVV, DateTime ExpiryDate);
    
}
