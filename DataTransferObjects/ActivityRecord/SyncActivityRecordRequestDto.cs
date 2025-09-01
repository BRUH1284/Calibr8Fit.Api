using Calibr8Fit.Api.Interfaces.DataTransferObjects;

namespace Calibr8Fit.Api.DataTransferObjects.ActivityRecord
{
    public class SyncActivityRecordRequestDto : ISyncRequestDto<AddActivityRecordRequestDto>
    {
        public DateTime LastSyncedAt { get; set; } = DateTime.MinValue;
        public List<AddActivityRecordRequestDto> ActivityRecords { get; set; } = [];


        IEnumerable<AddActivityRecordRequestDto> ISyncRequestDto<AddActivityRecordRequestDto>.AddEntityRequestDtos => ActivityRecords;
    }
}