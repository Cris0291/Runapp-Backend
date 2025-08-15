using Contracts.CustomerProfile.Response;
using RunApp.Domain.Common.ValueType;
using RunApp.Domain.CustomerProfileAggregate;

namespace RunApp.Api.Mappers.CustomerProfiles
{
    public static class CustomerProfileMapper
    {
        public static AddressResponse FromAddressToAddressResponse(this Address addressRequest)
        {
            return new AddressResponse(addressRequest.ZipCode, addressRequest.Street, addressRequest.City, addressRequest.Country, addressRequest.State);
        }
        public static AccountInfoResponse FromCustomerToAccountResponse(this CustomerProfile customerProfile)
        {
            return new AccountInfoResponse(customerProfile.Name, customerProfile.Email, customerProfile.NickName);
        }
        public static UserInfoResponse FromCustomerToUserInfo(this CustomerProfile customerProfile)
        {
            return new UserInfoResponse(customerProfile.Name, customerProfile.Email, customerProfile.NickName, customerProfile.ShippingAdress?.ZipCode, customerProfile.ShippingAdress?.Street, customerProfile.ShippingAdress?.City, customerProfile.ShippingAdress?.Country, customerProfile.ShippingAdress?.State, customerProfile.PaymentMethod?.HoldersName, customerProfile.PaymentMethod?.CardNumber, customerProfile.PaymentMethod?.CVV, customerProfile.PaymentMethod?.ExpiryDate);
        }
        public static UserResponseWithToken FromCustomerToUserDtoWithToken(this CustomerProfile customerProfile, string token, string refreshToken, DateTime refreshTokenExiprationDate)
        {
            return new UserResponseWithToken(customerProfile.Name, customerProfile.Email, customerProfile.NickName, customerProfile.ShippingAdress?.ZipCode, customerProfile.ShippingAdress?.Street, customerProfile.ShippingAdress?.City, customerProfile.ShippingAdress?.Country, customerProfile.ShippingAdress?.State, customerProfile.PaymentMethod?.HoldersName, customerProfile.PaymentMethod?.CardNumber, customerProfile.PaymentMethod?.CVV, customerProfile.PaymentMethod?.ExpiryDate, token , refreshToken, refreshTokenExiprationDate);
        }
        public static CardResponse FromCardToCardResponse(this Card card)
        {
            return new CardResponse(card.HoldersName, card.CardNumber, card.CVV, card.ExpiryDate);
        }
    }
    
}
