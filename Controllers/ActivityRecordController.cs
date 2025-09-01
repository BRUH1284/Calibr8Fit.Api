using Calibr8Fit.Api.Controllers.Abstract;
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
    ) : SyncableEntityControllerBase<
        ActivityRecord,
        ActivityRecordDto,
        Guid,
        IActivityRecordRepository,
        UpdateActivityRecordRequestDto,
        AddActivityRecordRequestDto,
        SyncActivityRecordRequestDto,
        SyncActivityRecordResponseDto
        >(
        currentUserService,
        activityRecordRepository,
        syncService
        )
    {
        private readonly IActivityValidationService _activityValidationService = activityValidationService;

        protected override ActivityRecordDto ToDto(ActivityRecord entity) => entity.ToActivityRecordDto();
        protected override ActivityRecord ToEntity(UpdateActivityRecordRequestDto updateDto, string userId) =>
            updateDto.ToActivityRecord(userId);
        protected override ActivityRecord ToEntity(AddActivityRecordRequestDto addDto, string userId) =>
            addDto.ToActivityRecord(userId);

        protected override SyncActivityRecordResponseDto ToSyncResponseDto(DateTime lastSyncedAt, List<ActivityRecord> entities) =>
           entities.ToSyncActivityRecordResponseDto(lastSyncedAt);

        [HttpPost("sync")]
        public Task<IActionResult> Sync([FromBody] SyncActivityRecordRequestDto requestDto) =>
            WithUser(async user =>
            {
                // Validate activity record links
                foreach (var record in requestDto.ActivityRecords)
                {
                    // Check if activity exists
                    if (!await _activityValidationService.ValidateActivityLinkAsync(user.Id, record.ActivityId))
                        return BadRequest($"Activity with id: {record.ActivityId} does not exist for user.");
                }

                return await base.Sync(requestDto);
            });
        [HttpPost]
        public override Task<IActionResult> Add([FromBody] AddActivityRecordRequestDto requestDto) =>
            WithUserId(async userId =>
            {
                // Validate activity record link
                if (!await _activityValidationService.ValidateActivityLinkAsync(userId, requestDto.ActivityId))
                    return BadRequest($"Activity with id: {requestDto.ActivityId} does not exist for user.");

                return await base.Add(requestDto);
            });
    }
}