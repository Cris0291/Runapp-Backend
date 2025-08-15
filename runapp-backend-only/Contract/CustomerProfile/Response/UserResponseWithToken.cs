namespace Contracts.CustomerProfile.Response
{
    public record UserResponseWithToken(string Name, string Email, string UserName, string? ZipCode, string? Address, string? City, string? Country, string? State, string? CardName, string? CardNumber, string? CVV, string? ExpiryDate, string token, string refreshToken, DateTime refreshTokenExiprationDate);
}
