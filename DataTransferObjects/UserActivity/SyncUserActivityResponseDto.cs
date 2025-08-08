namespace Calibr8Fit.Api.DataTransferObjects.UserActivity
{
    public class SyncUserActivityResponseDto
    {
        public required DateTime SyncedAt { get; set; }
        public required List<UserActivityDto> UserActivities { get; set; }
    }
}