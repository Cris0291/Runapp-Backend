namespace Contracts.CustomerProfile.Request
{
    public record PasswordDtoRequest(string OldPassword, string NewPassword, string Email);
}
