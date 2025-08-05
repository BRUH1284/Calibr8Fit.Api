using Calibr8Fit.Api.Interfaces.Model;
using Calibr8Fit.Api.Interfaces.Repository;
using Calibr8Fit.Api.Interfaces.Service;

namespace Calibr8Fit.Api.Services
{
    public class SyncService<T>(
        IUserSyncRepository<T> repository
        ) : ISyncService<T>
        where T : class, ISyncableUserEntity
    {
        private readonly IUserSyncRepository<T> _repository = repository;

        public async Task<DateTime> GetLastSyncedAtAsync(string userId)
        {
            // Return the last synced item date for the user
            return await _repository.GetLastSyncedAtAsync(userId);
        }
        public async Task<List<T>> Sync(string userId, List<T> entities, DateTime lastSyncedAt)
        {
            var entitiesDict = entities.ToDictionary(e => e.Id, e => e);
            var result = new List<T>();

            // Get all entities that have been modified since the last sync
            var modifiedEntities = await _repository.GetAllFromDateByUserIdAsync(lastSyncedAt, userId);
            // Add new entities to the result

            // Filter out entities that are already in the database
            foreach (var item in modifiedEntities)
            {
                if (entitiesDict.TryGetValue(item.Id, out var entity))
                {
                    if (entity.ModifiedAt < item.ModifiedAt)
                        entitiesDict.Remove(item.Id); // Remove the entity from the dictionary if it is older
                    else
                        result.Add(entity); // If the entity in the database is more recent, keep it in
                }
                else
                    result.Add(item); // Add modified entities that are not in the update list
            }

            // Update existing entities
            var updatedEntities = await _repository.UpdateRangeAsync(entitiesDict.Values);
            // Add updated entities to the result
            result.AddRange(updatedEntities);

            // Filter out updated entities that are already in the database
            var updatedIds = new HashSet<object[]>(updatedEntities.Select(e => e.Id));
            var newEntities = entities.Where(e => !updatedIds.Contains(e.Id)).ToList();

            // Add new entities
            var addedEntities = await _repository.AddRangeAsync(newEntities);
            // Add added entities to the result
            result.AddRange(addedEntities);

            return result;
        }
    }
}