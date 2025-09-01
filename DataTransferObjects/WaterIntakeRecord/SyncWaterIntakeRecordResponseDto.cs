using Calibr8Fit.Api.Interfaces.DataTransferObjects;

namespace Calibr8Fit.Api.DataTransferObjects.WaterIntakeRecord
{
    public class SyncWaterIntakeRecordResponseDto : ISyncResponseDto<WaterIntakeRecordDto>
    {
        public required DateTime LastSyncedAt { get; set; }
        public required List<WaterIntakeRecordDto> WaterIntakeRecords { get; set; }

        IEnumerable<WaterIntakeRecordDto> ISyncResponseDto<WaterIntakeRecordDto>.Entities => WaterIntakeRecords;
    }
}
