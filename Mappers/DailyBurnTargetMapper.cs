using Calibr8Fit.Api.DataTransferObjects.DailyBurnTarget;
using Calibr8Fit.Api.Models;

namespace Calibr8Fit.Api.Mappers
{
    public static class DailyBurnTargetMapper
    {
        public static DailyBurnTargetDto ToDailyBurnTargetDto(this DailyBurnTarget dailyBurnTarget)
        {
            return new DailyBurnTargetDto
            {
                Id = dailyBurnTarget.Id,
                ActivityId = dailyBurnTarget.ActivityId,
                Duration = dailyBurnTarget.Duration,
                ModifiedAt = dailyBurnTarget.ModifiedAt,
                Deleted = dailyBurnTarget.Deleted
            };
        }

        public static DailyBurnTarget ToDailyBurnTarget(this AddDailyBurnTargetRequestDto requestDto, string userId)
        {
            return new DailyBurnTarget
            {
                Id = requestDto.Id,
                UserId = userId,
                ActivityId = requestDto.ActivityId,
                Duration = requestDto.Duration,
                ModifiedAt = requestDto.ModifiedAt,
                Deleted = requestDto.Deleted
            };
        }

        public static DailyBurnTarget ToDailyBurnTarget(this DailyBurnTargetDto dailyBurnTargetDto, string userId)
        {
            return new DailyBurnTarget
            {
                Id = dailyBurnTargetDto.Id,
                UserId = userId,
                ActivityId = dailyBurnTargetDto.ActivityId,
                Duration = dailyBurnTargetDto.Duration,
                ModifiedAt = dailyBurnTargetDto.ModifiedAt,
                Deleted = dailyBurnTargetDto.Deleted
            };
        }

        public static DailyBurnTarget ToDailyBurnTarget(this UpdateDailyBurnTargetRequestDto requestDto, string userId)
        {
            return new DailyBurnTarget
            {
                Id = requestDto.Id,
                UserId = userId,
                ActivityId = requestDto.ActivityId,
                Duration = requestDto.Duration,
                ModifiedAt = requestDto.ModifiedAt,
                Deleted = requestDto.Deleted
            };
        }

        public static SyncDailyBurnTargetResponseDto ToSyncDailyBurnTargetResponseDto(
            this IEnumerable<DailyBurnTarget> dailyBurnTargets,
            DateTime syncedAt
        )
        {
            return new SyncDailyBurnTargetResponseDto
            {
                LastSyncedAt = syncedAt,
                DailyBurnTargets = dailyBurnTargets.Select(dbt => dbt.ToDailyBurnTargetDto()).ToList()
            };
        }
    }
}