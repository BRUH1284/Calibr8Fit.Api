using Calibr8Fit.Api.Interfaces.DataTransferObjects;

namespace Calibr8Fit.Api.DataTransferObjects.ConsumptionRecord
{
    public class SyncConsumptionRecordRequestDto : ISyncRequestDto<AddConsumptionRecordRequestDto>
    {
        public DateTime LastSyncedAt { get; set; } = DateTime.MinValue;
        public List<AddConsumptionRecordRequestDto> ConsumptionRecords { get; set; } = [];

        IEnumerable<AddConsumptionRecordRequestDto> ISyncRequestDto<AddConsumptionRecordRequestDto>.AddEntityRequestDtos => ConsumptionRecords;
    }
}
