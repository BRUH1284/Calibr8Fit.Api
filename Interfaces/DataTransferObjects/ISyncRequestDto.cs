namespace Calibr8Fit.Api.Interfaces.DataTransferObjects
{
    public interface ISyncRequestDto<TAddDto>
    {
        IEnumerable<TAddDto> AddEntityRequestDtos { get; }
        DateTime LastSyncedAt { get; }
    }
}