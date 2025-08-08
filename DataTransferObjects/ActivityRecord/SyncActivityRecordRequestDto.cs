namespace Calibr8Fit.Api.DataTransferObjects.ActivityRecord
{
    public class SyncActivityRecordRequestDto
    {
        public DateTime LastSyncedAt { get; set; } = DateTime.MinValue;

        public List<AddActivityRecordRequestDto> ActivityRecords { get; set; } = [];
    }
}