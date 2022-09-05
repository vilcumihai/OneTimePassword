using Microsoft.AspNetCore.Identity;

namespace OneTimePassword.Model.Entities
{
    public class User : IdentityUser
    {
        public string? OTPCode { get; set; }
        public DateTime OtpExpirationDate { get; set; }
    }
}
