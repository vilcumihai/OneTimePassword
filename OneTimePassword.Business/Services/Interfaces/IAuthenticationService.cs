using OneTimePassword.Model.Dtos;

namespace OneTimePassword.Business.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponseDto> Login(UserAuthenticationDto userForAuthentication);
    }
}
