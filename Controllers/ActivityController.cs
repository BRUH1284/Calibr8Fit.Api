using Calibr8Fit.Api.DataTransferObjects.Activity;
using Calibr8Fit.Api.DataTransferObjects.UserActivity;
using Calibr8Fit.Api.Interfaces.Repository;
using Calibr8Fit.Api.Interfaces.Service;
using Calibr8Fit.Api.Mappers;
using Calibr8Fit.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calibr8Fit.Api.Controllers
{
    [Route("api/activity")]
    [ApiController]
    public class ActivityController(
        IActivityRepository activityRepository,
        ICurrentUserService currentUserService,
        IUserActivityRepository userActivityRepository,
        ISyncService<UserActivity, Guid> syncService
        ) : ControllerBase
    {
        private readonly IActivityRepository _activityRepository = activityRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IUserActivityRepository _userActivityRepository = userActivityRepository;
        private readonly ISyncService<UserActivity, Guid> _syncService = syncService;

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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivityById(Guid id)
        {
            // Get activity by id from DB
            var activity = await _activityRepository.GetAsync(id);

            // If activity is null, return NotFound
            return activity is null
                ? NotFound($"Activity with id: {id} not found.")
                : Ok(activity.ToActivityDto());
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddActivity([FromBody] AddActivityRequestDto activityDto)
        {
            // Add new activity to DB
            var activity = await _activityRepository.AddAsync(activityDto.ToActivity());

            // If activity is null, return BadRequest
            return activity is null
                ? BadRequest("Failed to add activity.")
                : CreatedAtAction(
                    nameof(GetActivityById),
                    new { id = activity.Id },
                    activity.ToActivityDto());
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateActivity([FromBody] UpdateActivityRequestDto updateDto)
        {
            var activity = await _activityRepository.UpdateAsync(updateDto.ToActivity());

            // If activity is null, return NotFound
            return activity is null
                ? NotFound($"Activity with id: {updateDto.Id} not found.")
                : Ok(activity.ToActivityDto());
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteActivity(Guid id)
        {
            var activity = await _activityRepository.DeleteAsync(id);

            // If activity is null, return NotFound
            return activity is null
                ? NotFound($"Activity with id: {id} not found.")
                : NoContent();
        }
        [HttpGet("my/last-updated-at")]
        [Authorize]
        public async Task<IActionResult> GetLastUserUpdatedAt()
        {
            // Find user in DB
            var user = await _currentUserService.GetCurrentUserAsync(User);
            if (user is null) return Unauthorized("User not found.");

            // Get user's last updated time
            var lastUpdatedAt = await _syncService.GetLastSyncedAtAsync(user.Id);

            return Ok(lastUpdatedAt);
        }
        [HttpPost("my/sync")]
        [Authorize]
        public async Task<IActionResult> SyncUserActivities([FromBody] SyncUserActivitiesRequestDto requestDto)
        {
            // Find user in DB
            var user = await _currentUserService.GetCurrentUserAsync(User);
            if (user is null) return Unauthorized("User not found.");

            // Get synced activities from request DTOs
            var result = await _syncService.Sync(
                user.Id,
                requestDto.UserActivities.Select(dto => dto.ToUserActivity(user.Id)),
                requestDto.LastSyncedAt
            );

            return Ok(result.ToSyncUserActivityResponseDto(DateTime.UtcNow));
        }
        [HttpGet("my")]
        [Authorize]
        public async Task<IActionResult> GetAllUserActivities()
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
            var userActivity = await _userActivityRepository.GetByUserIdAndKeyAsync(user.Id, id);

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

            // Add new user activities to DB
            var addedActivity = await _userActivityRepository
                .AddAsync(userActivityDto.ToUserActivity(user.Id));

            // If activity is null, return BadRequest
            return addedActivity is null
                ? BadRequest("Failed to add user activity.")
                : CreatedAtAction(
                    nameof(GetActivityById),
                    new { id = addedActivity.Id },
                    addedActivity.ToUserActivityDto());
        }
        [HttpPut("my")]
        [Authorize]
        public async Task<IActionResult> UpdateUserActivity([FromBody] UpdateUserActivityRequestDto updateDto)
        {
            // Find user in DB
            var user = await _currentUserService.GetCurrentUserAsync(User);
            if (user is null) return Unauthorized("User not found.");

            // Update user activities
            var updatedActivity = await _userActivityRepository
                .UpdateByUserIdAsync(user.Id, updateDto.ToUserActivity(user.Id));

            // If activity is null, return NotFound
            return updatedActivity is null
                ? NotFound($"User activity with id: {updateDto.Id} not found.")
                : Ok(updatedActivity.ToUserActivityDto());
        }
        [HttpDelete("my")]
        [Authorize]
        public async Task<IActionResult> DeleteUserActivity([FromBody] DeleteUserActivityRequestDto deleteDto)
        {
            // Find user in DB
            var user = await _currentUserService.GetCurrentUserAsync(User);
            if (user is null) return Unauthorized("User not found.");

            // Delete user activities
            var deletedActivity = await _userActivityRepository
                .DeleteByUserIdAndIdAsync(user.Id, deleteDto.Id);

            // If no activities were deleted, return NotFound
            return deletedActivity is null
                ? NotFound($"User activity with id: {deleteDto.Id} not found.")
                : NoContent();
        }
    }
}