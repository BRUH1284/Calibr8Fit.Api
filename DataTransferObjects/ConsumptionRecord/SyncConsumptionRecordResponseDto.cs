using Calibr8Fit.Api.Interfaces.DataTransferObjects;

namespace Calibr8Fit.Api.DataTransferObjects.ConsumptionRecord
{
    public class SyncConsumptionRecordResponseDto : ISyncResponseDto<ConsumptionRecordDto>
    {
        public required DateTime LastSyncedAt { get; set; }
        public required List<ConsumptionRecordDto> ConsumptionRecords { get; set; }
        IEnumerable<ConsumptionRecordDto> ISyncResponseDto<ConsumptionRecordDto>.Entities => ConsumptionRecords;
    }
}
