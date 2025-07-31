using Calibr8Fit.Api.DataTransferObjects.Activity;
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
                UpdatedAt = userActivity.UpdatedAt
            };
        }
        public static UserActivity ToUserActivity(this AddUserActivityRequestDto requestDto, string userId)
        {
            return new UserActivity
            {
                UserId = userId,
                MajorHeading = requestDto.MajorHeading,
                MetValue = requestDto.MetValue,
                Description = requestDto.Description,
                UpdatedAt = requestDto.UpdatedAt
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
                UpdatedAt = activityDto.UpdatedAt
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
                UpdatedAt = requestDto.UpdatedAt
            };
        }
    }
}