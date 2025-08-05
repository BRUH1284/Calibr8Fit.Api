using Calibr8Fit.Api.Enums;
using Calibr8Fit.Api.Models;

namespace Calibr8Fit.Api.Interfaces.Repository
{
    public interface IDataVersionRepository : IRepositoryBase<DataVersion>
    {
        Task<DataVersion> AddOrUpdateAsync(DataResource dataResource);
        Task<DateTime?> LastUpdatedAtAsync(DataResource dataResource);
    }
}