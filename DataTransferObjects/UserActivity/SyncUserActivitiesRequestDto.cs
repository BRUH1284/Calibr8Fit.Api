using Calibr8Fit.Api.Interfaces.DataTransferObjects;

namespace Calibr8Fit.Api.DataTransferObjects.UserActivity
{
    public class SyncUserActivitiesRequestDto : ISyncRequestDto<AddUserActivityRequestDto>
    {
        public DateTime LastSyncedAt { get; set; } = DateTime.MinValue;

        public List<AddUserActivityRequestDto> UserActivities { get; set; } = [];

        IEnumerable<AddUserActivityRequestDto> ISyncRequestDto<AddUserActivityRequestDto>.AddEntityRequestDtos => UserActivities;
    }
}