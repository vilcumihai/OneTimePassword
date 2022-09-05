using OneTimePassword.Model.Dtos;

namespace OneTimePassword.Web.Services.Interfaces
{
    public interface IApiAuthenticationService
    {
        AuthenticationResponseDto Login(UserAuthenticationDto userAuthenticationDto);
    }
}
