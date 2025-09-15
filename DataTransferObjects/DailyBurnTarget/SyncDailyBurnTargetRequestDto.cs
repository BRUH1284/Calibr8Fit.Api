using Calibr8Fit.Api.Interfaces.DataTransferObjects;

namespace Calibr8Fit.Api.DataTransferObjects.DailyBurnTarget
{
    public class SyncDailyBurnTargetRequestDto : ISyncRequestDto<AddDailyBurnTargetRequestDto>
    {
        public DateTime LastSyncedAt { get; set; } = DateTime.MinValue;
        public List<AddDailyBurnTargetRequestDto> DailyBurnTargets { get; set; } = [];

        IEnumerable<AddDailyBurnTargetRequestDto> ISyncRequestDto<AddDailyBurnTargetRequestDto>.AddEntityRequestDtos => DailyBurnTargets;
    }
}