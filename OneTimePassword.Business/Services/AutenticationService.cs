using Microsoft.AspNetCore.Identity;
using OneTimePassword.Business.Handlers.Interfaces;
using OneTimePassword.Business.Services.Interfaces;
using OneTimePassword.Model.Dtos;
using OneTimePassword.Model.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace OneTimePassword.Business.Services
{
    public class AutenticationService : IAuthenticationService
    {
        private readonly IJwtHandler _jwtHandler;
        private readonly UserManager<User> _userManager;

        public AutenticationService(IJwtHandler jwtHandler, UserManager<User> userManager)
        {
            _jwtHandler = jwtHandler;
            _userManager = userManager;
        }

        public async Task<AuthenticationResponseDto> Login(UserAuthenticationDto userForAuthentication)
        {

            var user = await _userManager.FindByNameAsync(userForAuthentication.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, userForAuthentication.Password))
            {
                return new AuthenticationResponseDto { ErrorMessage = "Invalid Authentication" };
            }

            var signingCredentials = _jwtHandler.GetSigningCredentials();
            var claims = _jwtHandler.GetClaims(user);
            var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return new AuthenticationResponseDto { IsAuthSuccessful = true, Token = token };
        }
    }
}
