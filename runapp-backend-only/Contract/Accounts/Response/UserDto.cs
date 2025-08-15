namespace Contracts.Accounts.Response
{
    public record UserDto(string Name, string UserName, string Token, string Email, string RefreshToken, DateTime RefreshTokenExpirationDate);
}
