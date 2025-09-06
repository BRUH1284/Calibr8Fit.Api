using Calibr8Fit.Api.Data;
using Calibr8Fit.Api.Enums;
using Calibr8Fit.Api.Interfaces.Model;
using Calibr8Fit.Api.Interfaces.Repository;
using Calibr8Fit.Api.Interfaces.Repository.Base;

namespace Calibr8Fit.Api.Repository.Base
{
    public class DataVersionRepositoryBase<T, HT, TKey>(
        ApplicationDbContext context,
        IDataVersionRepository dataVersionRepository,
        DataResource dataResource
        ) : RepositoryBase<T, HT, TKey>(context), IDataVersionRepositoryBase<T, TKey>
        where T : class, IEntity<TKey>
        where HT : class, IEntity<TKey>
        where TKey : notnull
    {
        private readonly DataResource DataResource = dataResource;
        private readonly IDataVersionRepository _dataVersionRepository = dataVersionRepository;

        /// Get last updated time for the specific data resource
        public Task<DateTime?> LastUpdatedAtAsync() =>
            _dataVersionRepository.LastUpdatedAtAsync(DataResource);

        protected override async Task SaveChangesAsync()
        {
            await base.SaveChangesAsync();
            // Update data version
            await _dataVersionRepository.AddOrUpdateAsync(DataResource);
        }
    }

    public class DataVersionRepositoryBase<T, TKey>(
        ApplicationDbContext context,
        IDataVersionRepository dataVersionRepository,
        DataResource dataResource
        ) : DataVersionRepositoryBase<T, T, TKey>(
            context,
            dataVersionRepository,
            dataResource
            ), IDataVersionProvider
        where T : class, IEntity<TKey>
        where TKey : notnull
    {

    }
}