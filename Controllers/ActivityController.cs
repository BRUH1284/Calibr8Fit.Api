using Calibr8Fit.Api.DataTransferObjects.Activity;
using Calibr8Fit.Api.Interfaces.Repository;
using Calibr8Fit.Api.Interfaces.Service;
using Calibr8Fit.Api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calibr8Fit.Api.Controllers
{
    [Route("api/activity")]
    [ApiController]
    public class ActivityController(
        IActivityRepository activityRepository,
        ICurrentUserService currentUserService,
        IUserActivityRepository userActivityRepository
        ) : ControllerBase
    {
        private readonly IActivityRepository _activityRepository = activityRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IUserActivityRepository _userActivityRepository = userActivityRepository;

        [HttpGet("last-updated-at")]
        public async Task<IActionResult> GetLastUpdatedAt()
        {
            var lastUpdatedAt = await _activityRepository.LastUpdatedAtAsync();

            // If no last updated time, return NoContent
            return lastUpdatedAt is null
                ? NoContent()
                : Ok(lastUpdatedAt);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllActivities()
        {
            // Get all activities from DB
            var activities = await _activityRepository.GetAllAsync();
            var activityDtos = activities.Select(a => a.ToActivityDto());

            return Ok(activityDtos);
        }
        [HttpGet("{code}")]
        public async Task<IActionResult> GetActivityByCode(int code)
        {
            // Get activity by code from DB
            var activity = await _activityRepository.GetByCodeAsync(code);

            // If activity is null, return NotFound
            return activity is null
                ? NotFound($"Activity with code: {code} not found.")
                : Ok(activity.ToActivityDto());
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddActivity([FromBody] ActivityDto activityDto)
        {
            // Add new activity to DB
            var activity = await _activityRepository.AddAsync(activityDto.ToActivity());

            // If activity is null, return BadRequest
            return activity is null
                ? BadRequest("Failed to add activity.")
                : CreatedAtAction(nameof(GetActivityByCode), new { code = activity.Code }, activity.ToActivityDto());
        }
        [HttpPut("{code}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateActivity(int code, [FromBody] UpdateActivityRequestDto updateDto)
        {
            var activity = await _activityRepository.UpdateAsync(code, updateDto);

            // If activity is null, return NotFound
            return activity is null
                ? NotFound($"Activity with code: {code} not found.")
                : Ok(activity.ToActivityDto());
        }
        [HttpDelete("{code}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteActivity(int code)
        {
            var activity = await _activityRepository.DeleteAsync(code);

            // If activity is null, return NotFound
            return activity is null
                ? NotFound($"Activity with code: {code} not found.")
                : NoContent();
        }
        [HttpGet("my/checksum")]
        [Authorize]
        public async Task<IActionResult> GetUserActivitiesChecksum()
        {
            // Find user in DB
            var user = await _currentUserService.GetCurrentUserAsync(User);
            if (user is null) return Unauthorized("User not found.");

            // Get user's activities checksum
            var checksum = await _userActivityRepository.GenerateUserDataChecksumAsync(user.Id);

            // If checksum is null, return NotFound
            return checksum is null
                ? NotFound("No activities found for this user.")
                : Ok(checksum);
        }
        [HttpGet("my")]
        [Authorize]
        public async Task<IActionResult> GetUserActivities()
        {
            // Find user in DB
            var user = await _currentUserService.GetCurrentUserAsync(User);
            if (user is null) return Unauthorized("User not found.");

            // Get all user activities
            var userActivities = await _userActivityRepository.GetAllByUserIdAsync(user.Id);
            var userActivityDtos = userActivities.Select(ua => ua.ToUserActivityDto());

            return Ok(userActivityDtos);
        }
        [HttpGet("my/{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserActivityById(Guid id)
        {
            // Find user in DB
            var user = await _currentUserService.GetCurrentUserAsync(User);
            if (user is null) return Unauthorized("User not found.");

            // Get user activity by id
            var userActivity = await _userActivityRepository.GetByUserIdAndIdAsync(user.Id, id);

            // If activity is null, return NotFound
            return userActivity is null
                ? NotFound($"User activity with id: {id} not found.")
                : Ok(userActivity.ToUserActivityDto());
        }
        [HttpPost("my")]
        [Authorize]
        public async Task<IActionResult> AddUserActivity([FromBody] AddUserActivityRequestDto userActivityDto)
        {
            // Find user in DB
            var user = await _currentUserService.GetCurrentUserAsync(User);
            if (user is null) return Unauthorized("User not found.");

            // Add new user activity
            var addedActivity = await _userActivityRepository.AddAsync(userActivityDto.ToUserActivity(user.Id));

            // If activity is null, return BadRequest
            return addedActivity is null
                ? BadRequest("Failed to add user activity.")
                : CreatedAtAction(nameof(GetUserActivityById), new { id = addedActivity.Id }, addedActivity.ToUserActivityDto());
        }
        [HttpPut("my/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUserActivity(Guid id, [FromBody] UpdateActivityRequestDto updateDto)
        {
            // Find user in DB
            var user = await _currentUserService.GetCurrentUserAsync(User);
            if (user is null) return Unauthorized("User not found.");

            // Update user activity
            var updatedActivity = await _userActivityRepository.UpdateByUserIdAndIdAsync(user.Id, id, updateDto);

            // If activity is null, return NotFound
            return updatedActivity is null
                ? NotFound($"User activity with id: {id} not found.")
                : Ok(updatedActivity.ToUserActivityDto());
        }
        [HttpDelete("my/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUserActivity(Guid id)
        {
            // Find user in DB
            var user = await _currentUserService.GetCurrentUserAsync(User);
            if (user is null) return Unauthorized("User not found.");

            // Delete user activity
            var deletedActivity = await _userActivityRepository.DeleteByUserIdAndIdAsync(user.Id, id);

            // If activity is null, return NotFound
            return deletedActivity is null
                ? NotFound($"User activity with id: {id} not found.")
                : NoContent();
        }
    }
}