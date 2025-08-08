namespace Calibr8Fit.Api.DataTransferObjects.UserActivity
{
    public class SyncUserActivitiesRequestDto
    {
        public DateTime LastSyncedAt { get; set; } = DateTime.MinValue;

        public List<AddUserActivityRequestDto> UserActivities { get; set; } = [];
    }
}