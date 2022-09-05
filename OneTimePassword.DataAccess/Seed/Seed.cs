using Microsoft.AspNetCore.Identity;
using OneTimePassword.Model.Entities;

namespace OneTimePassword.DataAccess.Seed
{
    public class Seed
    {
        public static async Task EnsureUsers(UserManager<User> userManager)
        {
            await EnsureUser(userManager, "mihai.vilcu@test.com");
        }

        private static async Task EnsureUser(UserManager<User> userManager, string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user != null)
            {
                return;
            }

            user = new User
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true,
            };
            var result = await userManager.CreateAsync(user, "Test@1234");

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
        }
    }
}
