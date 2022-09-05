using Microsoft.Extensions.Options;
using OneTimePassword.Model.Dtos;
using OneTimePassword.Web.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace OneTimePassword.Web.Services
{
    public class ApiAuthenticationService : IApiAuthenticationService
    {
        private readonly HttpClient _httpClient;

        public ApiAuthenticationService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(configuration["ApiBaseAdress"].ToString());
        }

        public AuthenticationResponseDto Login(UserAuthenticationDto userAuthenticationDto)
        {
            var content = JsonSerializer.Serialize(userAuthenticationDto);
            var data = new StringContent(content, Encoding.UTF8, "application/json");
            var result = _httpClient.PostAsync("authentication/login", data).Result;
            string resultContent = result.Content.ReadAsStringAsync().Result;

            var response = JsonSerializer.Deserialize<AuthenticationResponseDto>(resultContent,
                new JsonSerializerOptions 
                { PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
                });

            return response;
        }
    }
}
