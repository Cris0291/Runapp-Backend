namespace RunnApp.Application.StoreOwnerProfiles.Commands.CreateStoreOwnerProfile
{
    public record AddresCommand(int ZipCode, string Street, string City, int BuildingNumber, string Country, string? AlternativeStreet, int? AlternativeBuildingNumber);

}
