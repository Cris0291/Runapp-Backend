namespace Contracts.StoreOwnerProfiles.Request
{
    public record AddressRequest(int ZipCode, string Street, string City, int BuildingNumber, string Country, string? AlternativeStreet = null, int? AlternativeBuildingNumber = null);
    
}
