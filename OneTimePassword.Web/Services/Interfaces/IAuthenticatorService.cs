using OneTimePassword.Model.Dtos;

namespace OneTimePassword.Web.Services.Interfaces
{
    public interface IAuthenticatorService
    {
        Task<UserOtpDto> GetOtpCodeAsync();
    }
}
