using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OneTimePassword.Business.Handlers.Interfaces
{
    public interface IJwtHandler
    {
        public SigningCredentials GetSigningCredentials();
        public List<Claim> GetClaims(IdentityUser user);
        public JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims);
    }
}
