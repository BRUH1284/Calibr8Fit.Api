using Calibr8Fit.Api.Interfaces.DataTransferObjects;

namespace Calibr8Fit.Api.DataTransferObjects.WeightRecord
{
    public class SyncWeightRecordResponseDto : ISyncResponseDto<WeightRecordDto>
    {
        public required DateTime LastSyncedAt { get; set; }
        public required List<WeightRecordDto> WeightRecords { get; set; }

        IEnumerable<WeightRecordDto> ISyncResponseDto<WeightRecordDto>.Entities => WeightRecords;
    }

}
