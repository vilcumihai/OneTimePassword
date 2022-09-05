using OneTimePassword.Business.Handlers.Interfaces;
using OneTimePassword.Business.Handlers;
using OneTimePassword.Business.Services.Interfaces;
using OneTimePassword.Business.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using OneTimePassword.Model.Entities;
using Microsoft.IdentityModel.Tokens;
using OneTimePassword.DataAccess;
using System.Text;

namespace OneTimePassword.Api.Configurations
{
    public static class ServicesConfigurationExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AutenticationService>();
            services.AddScoped<IJwtHandler, JwtHandler>();
            services.AddScoped<IOtpGeneratorService, OtpGeneratorService>();
        }

        public static void AddAuthentication(this IServiceCollection services, IConfigurationSection jwtSettings)
        {
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<DBContext>();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                        .GetBytes(jwtSettings.GetSection("securityKey").Value))
                };
            });
        }
    }
}
