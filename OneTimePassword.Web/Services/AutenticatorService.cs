using OneTimePassword.Model.Dtos;
using OneTimePassword.Web.Services.Interfaces;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace OneTimePassword.Web.Services
{
    public class AutenticatorService : IAuthenticatorService
    {
        private readonly HttpClient _httpClient;
        private readonly ProtectedLocalStorage _browserStorage;

        //TO DO: create base class for httpclient - code duplication :)
        //TO DO: const class for endpoints
        public AutenticatorService(HttpClient httpClient, IConfiguration configuration, ProtectedLocalStorage browserStorage)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(configuration["ApiBaseAdress"].ToString());
            _browserStorage = browserStorage;
        }

        public async Task<UserOtpDto> GetOtpCodeAsync()
        {
            var token = await _browserStorage.GetAsync<string>("Token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);
            var result = _httpClient.GetAsync("authentication/GenerateOTP").Result;
            string resultContent = result.Content.ReadAsStringAsync().Result;

            var response = JsonSerializer.Deserialize<UserOtpDto>(resultContent,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

            return response;
        }
    }
}
