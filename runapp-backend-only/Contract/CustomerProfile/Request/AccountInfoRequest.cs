namespace Contracts.CustomerProfile.Request
{
    public record AccountInfoRequest(string Name, string OldEmail, string NewEmail, string NickName);
}
