using Calibr8Fit.Api.Interfaces.Repository;
using Calibr8Fit.Api.Data;
using Calibr8Fit.Api.Enums;
using Calibr8Fit.Api.Models;

namespace Calibr8Fit.Api.Repository
{
    public class DataVersionRepository(ApplicationDbContext context) : RepositoryBase(context), IDataVersionRepository
    {
        public async Task<DataVersion> AddOrUpdateAsync(DataResource dataResource)
        {
            // Get existing data version
            var dataVersion = await _context.DataVersions.FindAsync(dataResource);

            // Add new data version or update existing
            if (dataVersion is null)
            {
                dataVersion = new DataVersion { DataResource = dataResource };
                await _context.DataVersions.AddAsync(dataVersion);
            }
            else
                dataVersion.LastUpdatedAt = DateTime.UtcNow;

            // Save changes
            await _context.SaveChangesAsync();
            return dataVersion;
        }

        public async Task<DateTime?> LastUpdatedAtAsync(DataResource dataResource)
        {
            var dataVersion = await _context.DataVersions.FindAsync(dataResource);

            // If there is no record, add new
            dataVersion ??= await AddOrUpdateAsync(dataResource);

            return dataVersion.LastUpdatedAt;
        }
    }
}