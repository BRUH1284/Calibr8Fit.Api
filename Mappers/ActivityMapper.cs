
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
                Code = activity.Code,
                MajorHeading = activity.MajorHeading,
                MetValue = activity.MetValue,
                Description = activity.Description
            };
        }
        public static Activity ToActivity(this ActivityDto activityDto)
        {
            return new Activity
            {
                Code = activityDto.Code,
                MajorHeading = activityDto.MajorHeading,
                MetValue = activityDto.MetValue,
                Description = activityDto.Description
            };
        }
    }
}