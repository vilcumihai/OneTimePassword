using OneTimePassword.Model.Dtos;

namespace OneTimePassword.Business.Services.Interfaces
{
    public interface IOtpGeneratorService
    {
        Task<UserOtpDto> GetCodeAsync(string email);
    }
}
