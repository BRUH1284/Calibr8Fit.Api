using Calibr8Fit.Api.Interfaces.DataTransferObjects;

namespace Calibr8Fit.Api.DataTransferObjects.UserActivity
{
    public class SyncUserActivityResponseDto : ISyncResponseDto<UserActivityDto>
    {
        public required DateTime LastSyncedAt { get; set; }
        public required List<UserActivityDto> UserActivities { get; set; }

        IEnumerable<UserActivityDto> ISyncResponseDto<UserActivityDto>.Entities => UserActivities;
    }
}