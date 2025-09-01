using Calibr8Fit.Api.Controllers.Abstract;
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
    public class UserProfileController(
        ICurrentUserService currentUserService,
        IUserProfileRepository userProfileRepository
        ) : AppControllerBase(currentUserService)
    {
        private readonly IUserProfileRepository _userProfileRepository = userProfileRepository;

        [HttpGet]
        [Authorize]
        public Task<IActionResult> GetMyProfile() =>
            WithUser(user => Task.FromResult<IActionResult>(
                user?.Profile is null
                    ? Unauthorized("User profile not found.")
                    : Ok(user.ToUserProfileDto())
            ));
        [HttpGet("settings")]
        [Authorize]
        public Task<IActionResult> GetMyProfileSettings() =>
            WithUser(user => Task.FromResult<IActionResult>(
                user?.Profile is null
                    ? Unauthorized("User profile not found.")
                    : Ok(user.ToUserProfileSettingsDto())
            ));
        [HttpPut("settings")]
        [Authorize]
        public Task<IActionResult> UpdateMyProfileSettingsAsync([FromBody] UpdateUserProfileSettingsRequestDto requestDto) =>
            WithUser(async user =>
            {
                if (user?.Profile is null) return Unauthorized("User profile not found.");

                // Update user profile settings
                await _userProfileRepository.UpdateAsync(requestDto.ToUserProfile(user));

                // Return updated profile settings
                return Ok(user.ToUserProfileSettingsDto());
            });
    }
}