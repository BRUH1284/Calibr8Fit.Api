using Calibr8Fit.Api.Controllers.Abstract;
using Calibr8Fit.Api.DataTransferObjects.User;
using Calibr8Fit.Api.Enums;
using Calibr8Fit.Api.Extensions;
using Calibr8Fit.Api.Interfaces.Repository;
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
        IUserRepository userRepository,
        IFriendshipService friendshipService,
        IUserProfileService userProfileService,
        IPathService pathService
        ) : UserControllerBase(currentUserService)
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IRepositoryBase<UserProfile, string> _userProfileRepository = userProfileRepository;
        private readonly IFriendshipService _friendshipService = friendshipService;
        private readonly IUserProfileService _userProfileService = userProfileService;
        private readonly IPathService _pathService = pathService;

        [HttpGet]
        [Authorize]
        public Task<IActionResult> GetMyProfile() =>
            WithUser(user =>
                user?.Profile is null
                    ? Unauthorized("User profile not found.")
                    : Ok(user.ToUserProfileDto(
                        _friendshipService.GetFriendsCountAsync(user.Id).Result,
                        0,
                        0,
                        FriendshipStatus.None, // Own profile, no friendship status
                        _pathService
                    ))
            );

        [HttpGet("{username}")]
        [Authorize]
        public Task<IActionResult> GetUserProfileByUsername(string username) =>
            WithUser(async u =>
            {
                var user = await _userRepository.GetByUsernameAsync(username);

                if (user?.Profile is null)
                    return NotFound($"User or user profile with {username} not found.");

                var friendsCount = await _friendshipService.GetFriendsCountAsync(user.Id);
                var friendshipStatus = await _friendshipService.GetFriendshipStatusAsync(u.UserName!, username);

                return Ok(user.ToUserProfileDto(
                    friendsCount,
                    0,
                    0,
                    friendshipStatus,
                    _pathService
                ));
            });

        [HttpGet("settings")]
        [Authorize]
        public Task<IActionResult> GetMyProfileSettings() =>
            WithUser(user =>
                user?.Profile is null
                    ? Unauthorized("User profile not found.")
                    : Ok(user.ToUserProfileSettingsDto(_pathService))
            );

        [HttpPut("settings")]
        [Authorize]
        public Task<IActionResult> UpdateMyProfileSettingsAsync([FromBody] UpdateUserProfileSettingsRequestDto requestDto) =>
            WithUser(async user =>
            {
                if (user?.Profile is null) return Unauthorized("User profile not found.");

                // Update user profile settings
                await _userProfileRepository.UpdateAsync(requestDto.ToUserProfile(user));

                // Return updated profile settings
                return Ok(user.ToUserProfileSettingsDto(_pathService));
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

                return Ok(user.GetProfilePictureUrl(_pathService));
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