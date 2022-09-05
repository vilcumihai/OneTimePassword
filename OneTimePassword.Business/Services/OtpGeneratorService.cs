using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using OneTimePassword.Business.Services.Interfaces;
using OneTimePassword.DataAccess;
using OneTimePassword.Model.Dtos;
using OneTimePassword.Model.Entities;
using OtpNet;

namespace OneTimePassword.Business.Services
{
    public class OtpGeneratorService : IOtpGeneratorService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public OtpGeneratorService(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<UserOtpDto> GetCodeAsync(string email)
        {
            var user = await _userManager.FindByNameAsync(email);

            if (DateTime.UtcNow > user.OtpExpirationDate)
            {
                var randomKeyLength = int.Parse(_configuration["RandomKeyLength"]);
                var tokenExpirationSeconds = int.Parse(_configuration["TokenExpirationSeconds"]);

                var otpCode = new Totp(KeyGeneration.GenerateRandomKey(randomKeyLength));
                user.OTPCode = otpCode.ComputeTotp();
                user.OtpExpirationDate = DateTime.UtcNow.AddSeconds(tokenExpirationSeconds);

                await _userManager.UpdateAsync(user);
            }

            return new UserOtpDto { Code = user.OTPCode, ExpirationDate = user.OtpExpirationDate };
        }
    }
}
