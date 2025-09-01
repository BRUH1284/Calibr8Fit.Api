using Calibr8Fit.Api.Interfaces.DataTransferObjects;

namespace Calibr8Fit.Api.DataTransferObjects.ActivityRecord
{
    public class SyncActivityRecordResponseDto : ISyncResponseDto<ActivityRecordDto>
    {
        public required DateTime LastSyncedAt { get; set; }
        public required List<ActivityRecordDto> ActivityRecords { get; set; }
        IEnumerable<ActivityRecordDto> ISyncResponseDto<ActivityRecordDto>.Entities => ActivityRecords;
    }
}