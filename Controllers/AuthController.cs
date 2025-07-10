using Calibr8Fit.Api.DataTransferObjects.Authentication;
using Calibr8Fit.Api.DataTransferObjects.Token;
using Calibr8Fit.Api.Interfaces.Service;
using Calibr8Fit.Api.Interfaces.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace Calibr8Fit.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRefreshTokenRepository _refreshTokenRepo;
        private readonly IAuthService _authService;
        private readonly ICurrentUserService _currentUserService;
        public AuthController(
            IRefreshTokenRepository refreshTokenRepo,
            IAuthService authService,
            ICurrentUserService currentUserService)
        {
            _refreshTokenRepo = refreshTokenRepo;
            _authService = authService;
            _currentUserService = currentUserService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Return user creation result
            var result = await _authService.RegisterUserAsync(registerDto);
            return result.Succeeded
                ? Ok(result.Data)
                : BadRequest(result.Errors);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Return login result
            var result = await _authService.LoginUserAsync(loginDto);
            return result.Succeeded
                ? Ok(result.Data)
                : Unauthorized(result.Errors);
        }
        [HttpPost("refresh-token")]
        [Authorize]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequestDto refreshTokenDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Return refresh token result
            var result = await _authService.RefreshTokenAsync(refreshTokenDto);
            return result.Succeeded
                ? Ok(result.Data)
                : Unauthorized(result.Errors);
        }
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout([FromQuery] string deviceId)
        {
            // Find user in DB
            var user = await _currentUserService.GetCurrentUserAsync(User);
            if (user == null) return Unauthorized("User not found.");

            // Delete refresh token for user and device
            if (await _refreshTokenRepo.DeleteAsync(user.Id, deviceId) == null)
                return NotFound("Refresh token not found for this user and device.");

            return NoContent();
        }
        [HttpPost("logout-all")]
        [Authorize]
        public async Task<IActionResult> LogoutAll()
        {
            // Find user in DB
            var user = await _currentUserService.GetCurrentUserAsync(User);
            if (user == null) return Unauthorized("User not found.");

            // Delete all refresh tokens for user
            if (await _refreshTokenRepo.DeleteAllAsync(user.Id) == null)
                return NotFound("No refresh tokens found for this user.");

            return NoContent();
        }
    }
}