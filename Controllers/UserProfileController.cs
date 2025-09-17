using Calibr8Fit.Api.Controllers.Abstract;
using Calibr8Fit.Api.DataTransferObjects.User;
using Calibr8Fit.Api.Extensions;
using Calibr8Fit.Api.Interfaces.Repository.Base;
using Calibr8Fit.Api.Interfaces.Service;
using Calibr8Fit.Api.Mappers;
using Calibr8Fit.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calibr8Fit.Api.Controllers
{
    [ApiController]
    [Route("api/user-profile")]
    public class UserProfileController(
        ICurrentUserService currentUserService,
        IRepositoryBase<UserProfile, string> userProfileRepository,
        IUserProfileService userProfileService,
        IPathService pathService
        ) : UserControllerBase(currentUserService)
    {
        private readonly IRepositoryBase<UserProfile, string> _userProfileRepository = userProfileRepository;
        private readonly IUserProfileService _userProfileService = userProfileService;
        private readonly IPathService _pathService = pathService;

        [HttpGet]
        [Authorize]
        public Task<IActionResult> GetMyProfile() =>
            WithUser(user =>
                user?.Profile is null
                    ? Unauthorized("User profile not found.")
                    : Ok(user.ToUserProfileDto(user.GetProfilePictureUrl(Request, _pathService)))
            );
        [HttpGet("settings")]
        [Authorize]
        public Task<IActionResult> GetMyProfileSettings() =>
            WithUser(user =>
                user?.Profile is null
                    ? Unauthorized("User profile not found.")
                    : Ok(user.ToUserProfileSettingsDto())
            );
        [HttpPut("settings")]
        [Authorize]
        public Task<IActionResult> UpdateMyProfileSettingsAsync([FromBody] UpdateUserProfileSettingsRequestDto requestDto) =>
            WithUser(async user =>
            {
                if (user?.Profile is null) return Unauthorized("User profile not found.");

                if (requestDto.ProfilePictureFileName is not null)
                {
                    var result = await _userProfileService.UpdateProfilePictureFileName(user, requestDto.ProfilePictureFileName);
                    if (!result.Succeeded)
                        return BadRequest(result.Errors);
                }

                // Update user profile settings
                await _userProfileRepository.UpdateAsync(requestDto.ToUserProfile(user));

                // Return updated profile settings
                return Ok(user.ToUserProfileSettingsDto());
            });

        // Profile Picture Management

        [HttpPost("profile-picture")]
        [Authorize]
        public Task<IActionResult> UploadProfilePicture(IFormFile file) =>
            WithUser(async user =>
            {
                var result = await _userProfileService.UploadProfilePictureAsync(user, file);

                if (!result.Succeeded)
                    return BadRequest(result.Errors);

                return Ok(user.GetProfilePictureUrl(Request, _pathService));
            });

        [HttpDelete("profile-picture")]
        [Authorize]
        public Task<IActionResult> DeleteMyProfilePicture() =>
            WithUser(async user =>
            {
                var result = await _userProfileService.DeleteMyProfilePictureAsync(user);

                if (!result.Succeeded)
                    return BadRequest(result.Errors?.FirstOrDefault() ?? "Unknown error");

                return Ok(new { message = "Profile picture deleted successfully." });
            });
    }
}