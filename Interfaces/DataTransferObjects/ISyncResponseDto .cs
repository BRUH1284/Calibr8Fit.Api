namespace Calibr8Fit.Api.Interfaces.DataTransferObjects
{
    public interface ISyncResponseDto<TDto>
    {
        DateTime LastSyncedAt { get; }
        IEnumerable<TDto> Entities { get; }
    }
}