

using Contracts.Common;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using RunApp.Domain.StoreOwnerProfileAggregate;
using RunApp.Domain.UserAggregate;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RunApp.Api.Services
{
    public class JwtServiceGenerator(IConfiguration configuration) : IJwtServiceGenerator
    {
        private readonly IConfiguration _configuration = configuration;
        public string GenerateJwtToken(AppUser? user = null, StoreOwnerProfile? storeProfile = null, CustomClaim[]? customClaims = null)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

           
                var claims = GenerateClaims(user, storeProfile, customClaims);

            var securityToken = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims,expires: DateTime.UtcNow.AddMinutes(15), signingCredentials: signCredentials);
           
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            string token = jwtTokenHandler.WriteToken(securityToken);
            return token;
        }

        public ClaimsPrincipal? GetPrincipalFromJwtToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidAudience = _configuration["Jwt:Audience"],
                ValidateIssuer = true,
                ValidIssuer = _configuration["Jwt:Issuer"],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),

                ValidateLifetime = false
            };

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            ClaimsPrincipal claimsPrincipal =  jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if(securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCulture))
            {
                throw new SecurityTokenException("Invalid Token");
            }

            return claimsPrincipal;

        }
        private List<Claim> GenerateClaims(AppUser? user, StoreOwnerProfile? storeProfile, CustomClaim[]? customClaims = null)
        {
            var claims = new List<Claim>();

            if (user != null)
            {
                claims = new List<Claim>
                { 

                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.NickName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
                    new Claim("UserId", user.Id.ToString())
                };
             }
            else
            {

                claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, storeProfile.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
                    new Claim("UserId", storeProfile.Id.ToString()),
                    new Claim("StoreProfileId", storeProfile.StoreProfileId.ToString())
                };

                if (customClaims != null)
                {
                    foreach (var customClaim in customClaims)
                    {
                        claims.Add(new Claim(customClaim.Key, customClaim.Value));
                    }
                }
            }
          
            return claims;
        }
    }
}
