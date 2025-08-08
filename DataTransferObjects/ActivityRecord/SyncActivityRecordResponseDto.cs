namespace Calibr8Fit.Api.DataTransferObjects.ActivityRecord
{
    public class SyncActivityRecordResponseDto
    {
        public required DateTime LastSyncedAt { get; set; }
        public required List<ActivityRecordDto> ActivityRecords { get; set; }
    }
}