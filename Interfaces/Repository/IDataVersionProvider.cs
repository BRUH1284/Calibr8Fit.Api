using Calibr8Fit.Api.Enums;

namespace Calibr8Fit.Api.Interfaces.Repository
{
    public interface IDataVersionProvider
    {
        public Task<DateTime?> LastUpdatedAtAsync();
    }
}