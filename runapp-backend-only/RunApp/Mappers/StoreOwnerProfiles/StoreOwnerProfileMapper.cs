using Contracts.StoreOwnerProfiles.Request;
using Contracts.StoreOwnerProfiles.Response;
using RunApp.Domain.StoreOwnerProfileAggregate;
using RunApp.Domain.StoreOwnerProfileAggregate.ValueTypes;
using RunnApp.Application.StoreOwnerProfiles.Commands.CreateStoreOwnerProfile;

namespace RunApp.Api.Mappers.StoreOwnerProfiles
{
    public static class StoreOwnerProfileMapper
    {
        public static AddresCommand FromAdressRequestToAdressCommand(this AddressRequest addressRequest)
        {
            return new AddresCommand(addressRequest.ZipCode, addressRequest.Street, addressRequest.City, addressRequest.BuildingNumber, addressRequest.Country, addressRequest.AlternativeStreet, addressRequest.AlternativeBuildingNumber.Value);
        }
        public static CardCommand FromCardRequestToCardCommand(this CardRequest cardRequest)
        {
            return new CardCommand(cardRequest.HoldersName, cardRequest.CardNumber, cardRequest.CVV, cardRequest.ExpiryDate);
        }
        public static StoreOwnerProfileResponse FromProfileRequestToProfileResponse(this StoreOwnerProfile profileRequest, string token)
        {
            return new StoreOwnerProfileResponse(profileRequest.StoreProfileId, profileRequest.Id,
                                                 profileRequest.StoreName, profileRequest.BussinesAdress.FromAddresRequestToAddressResponse(),
                                                 profileRequest.SalesLevel.Name,profileRequest.IsAccountPaid,profileRequest.TotalProductsSold,profileRequest.TotalSalesInCash,profileRequest.TotalStock,profileRequest.InitialInvestment,token);
        }
        public static AddressResponse FromAddresRequestToAddressResponse(this Address address)
        {
            return new AddressResponse(address.ZipCode,address.Street,address.City, address.BuildingNumber,address.Country);
        }
    }
}
