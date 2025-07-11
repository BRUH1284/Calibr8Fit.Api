using Calibr8Fit.Api.DataTransferObjects.User;
using Calibr8Fit.Api.Interfaces.Repository;
using Calibr8Fit.Api.Interfaces.Service;
using Calibr8Fit.Api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calibr8Fit.Api.Controllers
{
    [ApiController]
    [Route("api/user-profile")]
    public class UserProfileController : ControllerBase
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserProfileRepository _userProfileRepository;
        public UserProfileController(
            ICurrentUserService currentUserService,
            IUserProfileRepository userProfileRepository)
        {
            _currentUserService = currentUserService;
            _userProfileRepository = userProfileRepository;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetMyProfileAsync()
        {
            // Find user in DB
            var user = await _currentUserService.GetCurrentUserAsync(User);
            if (user?.Profile == null) return Unauthorized("User profile not found.");

            return Ok(user.ToUserProfileDto());
        }
        [HttpGet("settings")]
        [Authorize]
        public async Task<IActionResult> GetMyProfileSettingsAsync()
        {
            // Find user in DB
            var user = await _currentUserService.GetCurrentUserAsync(User);
            if (user?.Profile == null) return Unauthorized("User profile not found.");

            return Ok(user.ToUserProfileSettingsDto());
        }
        [HttpPut("settings")]
        [Authorize]
        public async Task<IActionResult> UpdateMyProfileSettingsAsync([FromBody] UserProfileSettingsDto request)
        {
            // Find user in DB
            var user = await _currentUserService.GetCurrentUserAsync(User);
            if (user?.Profile == null) return Unauthorized("User profile not found.");

            // Update user profile settings
            await _userProfileRepository.UpdateAsync(user.Id, request);

            // Return updated profile settings
            return Ok(user.ToUserProfileSettingsDto());
        }
    }
}