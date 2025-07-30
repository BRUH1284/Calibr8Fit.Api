using Calibr8Fit.Api.Enums;

namespace Calibr8Fit.Api.Interfaces.Repository
{
    public interface IDataVersionProvider
    {
        IDataVersionRepository DataVersionRepository { get; }
        DataResource DataResource { get; }
        public async Task<DateTime?> LastUpdatedAtAsync()
        {
            // Get last updated time for the specific data resource
            return await DataVersionRepository.LastUpdatedAtAsync(DataResource);
        }
    }
}