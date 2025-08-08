using Calibr8Fit.Api.DataTransferObjects.ActivityRecord;
using Calibr8Fit.Api.Interfaces.Repository;
using Calibr8Fit.Api.Interfaces.Service;
using Calibr8Fit.Api.Mappers;
using Calibr8Fit.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calibr8Fit.Api.Controllers
{
    [Route("api/activity-record")]
    [ApiController]
    [Authorize]
    public class ActivityRecordController(
        IActivityRecordRepository activityRecordRepository,
        ICurrentUserService currentUserService,
        ISyncService<ActivityRecord, Guid> syncService,
        IActivityValidationService activityValidationService
    ) : ControllerBase
    {
        private readonly IActivityRecordRepository _activityRecordRepository = activityRecordRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly ISyncService<ActivityRecord, Guid> _syncService = syncService;
        private readonly IActivityValidationService _activityValidationService = activityValidationService;

        [HttpGet("last-updated-at")]
        public async Task<IActionResult> GetLastUpdatedAt()
        {
            // Find user in DB
            var user = await _currentUserService.GetCurrentUserAsync(User);
            if (user is null) return Unauthorized("User not found.");

            // Get last updated time
            var lastUpdatedAt = await _syncService.GetLastSyncedAtAsync(user.Id);
            return Ok(lastUpdatedAt);
        }
        [HttpPost("sync")]
        public async Task<IActionResult> SyncUserActivityRecords([FromBody] SyncActivityRecordRequestDto requestDto)
        {
            // Find user in DB
            var user = await _currentUserService.GetCurrentUserAsync(User);
            if (user is null) return Unauthorized("User not found.");

            // Validate activity record links
            foreach (var record in requestDto.ActivityRecords)
            {
                // Check if activity exists
                if (!await _activityValidationService.ValidateActivityLinkAsync(user.Id, record.ActivityId))
                    return BadRequest($"Activity with id: {record.ActivityId} does not exist for user.");
            }

            // Get synced activity records from request DTOs
            var result = await _syncService.Sync(
                user.Id,
                requestDto.ActivityRecords.Select(ar => ar.ToActivityRecord(user.Id)),
                requestDto.LastSyncedAt
            );

            return Ok(result.ToSyncActivityRecordResponseDto(DateTime.UtcNow));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllActivityRecords()
        {
            // Find user in DB
            var user = await _currentUserService.GetCurrentUserAsync(User);
            if (user is null) return Unauthorized("User not found.");

            // Get all activity records for the user
            var activityRecords = await _activityRecordRepository.GetAllByUserIdAsync(user.Id);
            var activityRecordDtos = activityRecords.Select(ar => ar.ToActivityRecordDto());

            return Ok(activityRecordDtos);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivityRecordById(Guid id)
        {
            // Find user in DB
            var user = await _currentUserService.GetCurrentUserAsync(User);
            if (user is null) return Unauthorized("User not found.");

            // Get activity record by id
            var activityRecord = await _activityRecordRepository.GetByUserIdAndKeyAsync(user.Id, id);

            // If record is null, return NotFound
            return activityRecord is null
                ? NotFound($"Activity record with id: {id} not found.")
                : Ok(activityRecord.ToActivityRecordDto());
        }
        [HttpPost]
        public async Task<IActionResult> AddActivityRecord([FromBody] AddActivityRecordRequestDto requestDto)
        {
            // Find user in DB
            var user = await _currentUserService.GetCurrentUserAsync(User);
            if (user is null) return Unauthorized("User not found.");

            // Validate activity record link
            if (!await _activityValidationService.ValidateActivityLinkAsync(user.Id, requestDto.ActivityId))
                return BadRequest($"Activity with id: {requestDto.ActivityId} does not exist for user.");

            // Add new activity record to DB
            var addedActivityRecord = await _activityRecordRepository
                .AddAsync(requestDto.ToActivityRecord(user.Id));

            // If record is null, return BadRequest
            return addedActivityRecord is null
                ? BadRequest("Failed to add activity record.")
                : CreatedAtAction(
                    nameof(GetActivityRecordById),
                    new { id = addedActivityRecord.Id },
                    addedActivityRecord.ToActivityRecordDto()
                );
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateActivityRecord(Guid id, [FromBody] UpdateActivityRecordRequestDto requestDto)
        {
            // Find user in DB
            var user = await _currentUserService.GetCurrentUserAsync(User);
            if (user is null) return Unauthorized("User not found.");

            // Update existing activity record
            var updatedActivityRecord = await _activityRecordRepository
                .UpdateByUserIdAsync(user.Id, requestDto.ToActivityRecord(user.Id));

            // If record is null, return NotFound
            return updatedActivityRecord is null
                ? NotFound($"Activity record with id: {id} not found.")
                : Ok(updatedActivityRecord.ToActivityRecordDto());
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivityRecord(Guid id)
        {
            // Find user in DB
            var user = await _currentUserService.GetCurrentUserAsync(User);
            if (user is null) return Unauthorized("User not found.");

            // Delete activity record by id
            var deletedActivityRecord = await _activityRecordRepository
                .DeleteByUserIdAndIdAsync(user.Id, id);

            // If no record was deleted, return NotFound
            return deletedActivityRecord is null
                ? NotFound($"Activity record with id: {id} not found.")
                : NoContent();
        }
    }
}