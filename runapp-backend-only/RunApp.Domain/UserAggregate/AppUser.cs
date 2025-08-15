using Microsoft.AspNetCore.Identity;

namespace RunApp.Domain.UserAggregate
{
    public class AppUser : IdentityUser<Guid>
    {
        public string NickName { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpirationDate { get; set; }
    }
}
