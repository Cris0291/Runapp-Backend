namespace RunnApp.Application.StoreOwnerProfiles.Commands.CreateStoreOwnerProfile
{
    public record CardCommand(string HoldersName, int CardNumber, int CVV, DateTime ExpiryDate);
}
