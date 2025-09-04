using Calibr8Fit.Api.Interfaces.DataTransferObjects;

namespace Calibr8Fit.Api.DataTransferObjects.WeightRecord
{
    public class SyncWeightRecordRequestDto : ISyncRequestDto<AddWeightRecordRequestDto>
    {
        public required DateTime LastSyncedAt { get; set; } = DateTime.MinValue;
        public required List<AddWeightRecordRequestDto> WeightRecords { get; set; } = [];

        IEnumerable<AddWeightRecordRequestDto> ISyncRequestDto<AddWeightRecordRequestDto>.AddEntityRequestDtos => WeightRecords;
    }

}
