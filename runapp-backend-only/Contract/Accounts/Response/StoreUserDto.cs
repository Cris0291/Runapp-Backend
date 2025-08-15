namespace Contracts.Accounts.Response
{
    public record StoreUserDto(string UserName, string NickName, string Email,string Storename, string SalesLevel, Guid StoreId, string Token);
   
}
