using Calibr8Fit.Api.Interfaces.DataTransferObjects;

namespace Calibr8Fit.Api.DataTransferObjects.WaterIntakeRecord
{
    public class SyncWaterIntakeRecordRequestDto : ISyncRequestDto<AddWaterIntakeRecordRequestDto>
    {
        public DateTime LastSyncedAt { get; set; } = DateTime.MinValue;
        public List<AddWaterIntakeRecordRequestDto> WaterIntakeRecords { get; set; } = [];

        IEnumerable<AddWaterIntakeRecordRequestDto> ISyncRequestDto<AddWaterIntakeRecordRequestDto>.AddEntityRequestDtos => WaterIntakeRecords;
    }
}
