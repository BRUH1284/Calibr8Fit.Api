using Calibr8Fit.Api.DataTransferObjects.ActivityRecord;
using Calibr8Fit.Api.Models;

namespace Calibr8Fit.Api.Mappers
{
    public static class ActivityRecordMapper
    {
        public static ActivityRecordDto ToActivityRecordDto(this ActivityRecord activityRecord)
        {
            return new ActivityRecordDto
            {
                Id = activityRecord.Id,
                ActivityId = activityRecord.ActivityId,
                Duration = activityRecord.Duration,
                CaloriesBurned = activityRecord.CaloriesBurned,
                Time = activityRecord.Time,
                ModifiedAt = activityRecord.ModifiedAt,
                Deleted = activityRecord.Deleted
            };
        }

        public static ActivityRecord ToActivityRecord(this AddActivityRecordRequestDto requestDto, string userId)
        {
            return new ActivityRecord
            {
                Id = requestDto.Id,
                UserId = userId,
                ActivityId = requestDto.ActivityId,
                Duration = requestDto.Duration,
                CaloriesBurned = requestDto.CaloriesBurned,
                Time = requestDto.Time,
                ModifiedAt = requestDto.ModifiedAt,
                Deleted = requestDto.Deleted
            };
        }

        public static ActivityRecord ToActivityRecord(this ActivityRecordDto activityRecordDto, string userId)
        {
            return new ActivityRecord
            {
                Id = activityRecordDto.Id,
                UserId = userId,
                ActivityId = activityRecordDto.ActivityId,
                Duration = activityRecordDto.Duration,
                CaloriesBurned = activityRecordDto.CaloriesBurned,
                Time = activityRecordDto.Time,
                ModifiedAt = activityRecordDto.ModifiedAt,
                Deleted = activityRecordDto.Deleted
            };
        }

        public static ActivityRecord ToActivityRecord(this UpdateActivityRecordRequestDto requestDto, string userId)
        {
            return new ActivityRecord
            {
                Id = requestDto.Id,
                UserId = userId,
                Duration = requestDto.Duration,
                CaloriesBurned = requestDto.CaloriesBurned,
                Time = requestDto.Time,
                ModifiedAt = requestDto.ModifiedAt,
                Deleted = requestDto.Deleted
            };
        }

        public static SyncActivityRecordResponseDto ToSyncActivityRecordResponseDto(
            this IEnumerable<ActivityRecord> activityRecords,
            DateTime syncedAt
        )
        {
            return new SyncActivityRecordResponseDto
            {
                LastSyncedAt = syncedAt,
                ActivityRecords = activityRecords.Select(ar => ar.ToActivityRecordDto()).ToList()
            };
        }
    }
}
