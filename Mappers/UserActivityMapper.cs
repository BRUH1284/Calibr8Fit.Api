using Calibr8Fit.Api.DataTransferObjects.UserActivity;
using Calibr8Fit.Api.Models;

namespace Calibr8Fit.Api.Mappers
{
    public static class UserActivityMapper
    {
        public static UserActivityDto ToUserActivityDto(this UserActivity userActivity)
        {
            return new UserActivityDto
            {
                Id = userActivity.Id,
                MajorHeading = userActivity.MajorHeading,
                MetValue = userActivity.MetValue,
                Description = userActivity.Description,
                ModifiedAt = userActivity.ModifiedAt,
                Deleted = userActivity.Deleted
            };
        }
        public static UserActivity ToUserActivity(this AddUserActivityRequestDto requestDto, string userId)
        {
            return new UserActivity
            {
                Id = requestDto.Id,
                UserId = userId,
                MajorHeading = requestDto.MajorHeading,
                MetValue = requestDto.MetValue,
                Description = requestDto.Description,
                ModifiedAt = requestDto.ModifiedAt,
                Deleted = requestDto.Deleted
            };
        }
        public static UserActivity ToUserActivity(this UserActivityDto activityDto, string userId)
        {
            return new UserActivity
            {
                UserId = userId,
                Id = activityDto.Id,
                MajorHeading = activityDto.MajorHeading,
                MetValue = activityDto.MetValue,
                Description = activityDto.Description,
                ModifiedAt = activityDto.ModifiedAt,
                Deleted = activityDto.Deleted
            };
        }
        public static UserActivity ToUserActivity(this UpdateUserActivityRequestDto requestDto, string userId)
        {
            return new UserActivity
            {
                UserId = userId,
                Id = requestDto.Id,
                MajorHeading = requestDto.MajorHeading,
                MetValue = requestDto.MetValue,
                Description = requestDto.Description,
                ModifiedAt = requestDto.ModifiedAt,
                Deleted = requestDto.Deleted
            };
        }
        public static SyncUserActivityResponseDto ToSyncUserActivityResponseDto(
            this IEnumerable<UserActivity> userActivities,
            DateTime syncedAt
            )
        {
            return new SyncUserActivityResponseDto
            {
                LastSyncedAt = syncedAt,
                UserActivities = userActivities.Select(ua => ua.ToUserActivityDto()).ToList()
            };
        }
    }
}