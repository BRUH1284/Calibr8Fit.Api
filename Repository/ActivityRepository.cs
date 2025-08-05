using Calibr8Fit.Api.Data;
using Calibr8Fit.Api.Enums;
using Calibr8Fit.Api.Interfaces.Repository;
using Calibr8Fit.Api.Models;

namespace Calibr8Fit.Api.Repository
{
    public class ActivityRepository(
        IDataVersionRepository dataVersionRepository,
        ApplicationDbContext context
        ) : RepositoryBase<Activity>(context), IActivityRepository
    {
        private static readonly DataResource DataResource = DataResource.Activities;
        IDataVersionRepository IDataVersionProvider.DataVersionRepository => dataVersionRepository;
        DataResource IDataVersionProvider.DataResource => DataResource;
        protected override async Task SaveChangesAsync()
        {
            await base.SaveChangesAsync();
            // Update data version
            await dataVersionRepository.AddOrUpdateAsync(DataResource);
        }
    }
}