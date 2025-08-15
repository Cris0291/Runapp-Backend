namespace Contracts.CustomerProfile.Response
{
    public record UserInfoResponse(string Name, string Email, string UserName, string? ZipCode, string? Address, string? City, string? Country, string? State, string? CardName, string? CardNumber, string? CVV, string? ExpiryDate);
}
