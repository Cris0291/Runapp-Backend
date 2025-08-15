using Contracts.Common;
using RunApp.Domain.StoreOwnerProfileAggregate;
using RunApp.Domain.UserAggregate;
using System.Security.Claims;

namespace RunApp.Api.Services
{
    public interface IJwtServiceGenerator
    {
        string GenerateJwtToken(AppUser? user = null, StoreOwnerProfile? storeProfile = null, CustomClaim[]? customClaims = null);
        ClaimsPrincipal? GetPrincipalFromJwtToken(string? token);
    }
}