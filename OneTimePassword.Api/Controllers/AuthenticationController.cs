using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneTimePassword.Business.Services.Interfaces;
using OneTimePassword.Model.Dtos;
using System.Security.Claims;

namespace OneTimePassword.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IOtpGeneratorService _otpGeneratorService;

        public AuthenticationController(IAuthenticationService authenticationService, IOtpGeneratorService otpGeneratorService)
        {
            _authenticationService = authenticationService;
            _otpGeneratorService = otpGeneratorService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserAuthenticationDto userForAuthentication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthenticationResponseDto { ErrorMessage = "Email & Password required." });
            }

            var loginResponse = await _authenticationService.Login(userForAuthentication);

            if (loginResponse.IsAuthSuccessful)
            {
                return Unauthorized(loginResponse);
            }

            return Ok(loginResponse);
        }

        [Authorize]
        [HttpGet("GenerateOtp")]
        public async Task<IActionResult> GenerateOTP()
        {
            var email = User.FindFirst(ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest();
            }

            var otpCode = await _otpGeneratorService.GetCodeAsync(email);
            return Ok(otpCode);
        }

    }
}
