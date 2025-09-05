using Calibr8Fit.Api.DataTransferObjects.Authentication;
using Calibr8Fit.Api.DataTransferObjects.Token;
using Calibr8Fit.Api.Interfaces.Service;
using Calibr8Fit.Api.Interfaces.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Calibr8Fit.Api.Controllers.Abstract;
namespace Calibr8Fit.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(
        IRefreshTokenRepository refreshTokenRepo,
        IAuthService authService,
        ICurrentUserService currentUserService
        ) : UserControllerBase(currentUserService)
    {
        private readonly IRefreshTokenRepository _refreshTokenRepo = refreshTokenRepo;
        private readonly IAuthService _authService = authService;

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto registerDto)
        {
            // Return user creation result
            var result = await _authService.RegisterUserAsync(registerDto);
            return result.Succeeded
                ? Ok(result.Data)
                : BadRequest(result.Errors);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            // Return login result
            var result = await _authService.LoginUserAsync(loginDto);
            return result.Succeeded
                ? Ok(result.Data)
                : Unauthorized(result.Errors);
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequestDto refreshTokenDto)
        {
            // Return refresh token result
            var result = await _authService.RefreshTokenAsync(refreshTokenDto);
            return result.Succeeded
                ? Ok(result.Data)
                : Unauthorized(result.Errors);
        }
        [HttpPost("logout")]
        [Authorize]
        public Task<IActionResult> Logout([FromQuery] string deviceId) =>
            WithUser(async user =>
            {
                // Delete refresh token for user and device
                return await _refreshTokenRepo.DeleteAsync([user.Id, deviceId]) is null
                    ? NotFound("Refresh token not found for this user and device.")
                    : NoContent();
            });
        [HttpPost("logout-all")]
        [Authorize]
        public Task<IActionResult> LogoutAll() =>
            WithUser(async user =>
            {
                // Delete all refresh tokens for user
                return await _refreshTokenRepo.DeleteAllByUserIdAsync(user.Id) is null
                    ? NotFound("No refresh tokens found for this user.")
                    : NoContent();
            });
    }
}