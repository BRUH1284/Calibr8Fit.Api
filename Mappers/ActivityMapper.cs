
using Calibr8Fit.Api.DataTransferObjects.Activity;
using Calibr8Fit.Api.Models;

namespace Calibr8Fit.Api.Mappers
{
    public static class ActivityMapper
    {
        public static ActivityDto ToActivityDto(this Activity activity)
        {
            return new ActivityDto
            {
                Id = activity.Id,
                MajorHeading = activity.MajorHeading,
                MetValue = activity.MetValue,
                Description = activity.Description
            };
        }
        public static Activity ToActivity(this ActivityDto activityDto)
        {
            return new Activity
            {
                Id = activityDto.Id,
                MajorHeading = activityDto.MajorHeading,
                MetValue = activityDto.MetValue,
                Description = activityDto.Description
            };
        }
        public static Activity ToActivity(this AddActivityRequestDto activityDto)
        {
            return new Activity
            {
                MajorHeading = activityDto.MajorHeading,
                MetValue = activityDto.MetValue,
                Description = activityDto.Description
            };
        }
    }
}