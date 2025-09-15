using Calibr8Fit.Api.Interfaces.DataTransferObjects;

namespace Calibr8Fit.Api.DataTransferObjects.DailyBurnTarget
{
    public class SyncDailyBurnTargetResponseDto : ISyncResponseDto<DailyBurnTargetDto>
    {
        public required DateTime LastSyncedAt { get; set; }
        public required List<DailyBurnTargetDto> DailyBurnTargets { get; set; }
        IEnumerable<DailyBurnTargetDto> ISyncResponseDto<DailyBurnTargetDto>.Entities => DailyBurnTargets;
    }
}