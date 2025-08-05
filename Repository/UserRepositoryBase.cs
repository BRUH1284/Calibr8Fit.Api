using Calibr8Fit.Api.Data;
using Calibr8Fit.Api.Interfaces.Model;
using Calibr8Fit.Api.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Calibr8Fit.Api.Repository
{
    public abstract class UserRepositoryBase<T>(
        ApplicationDbContext context
        ) : RepositoryBase<T>(context), IUserRepositoryBase<T>
        where T : class, IUserEntity
    {
        public async Task<List<T>> GetAllByUserIdAsync(string userId)
        {
            // Get all entities by userId
            return await _dbSet.Where(e => e.UserId.Equals(userId))
                .ToListAsync();
        }

        public async Task<T?> GetByUserIdAndKeyAsync(string userId, params object[] key)
        {
            // Get entity by userId and key
            return await _dbSet
                .FirstOrDefaultAsync(e => e.UserId.Equals(userId) && e.Id.Equals(key));
        }

        public async Task<List<T>> GetRangeByUserIdAsync(string userId, IEnumerable<object[]> keys)
        {
            // Get range of entities by userId and keys
            return await _dbSet
                .Where(e => e.UserId.Equals(userId) && keys.Contains(e.Id))
                .ToListAsync();
        }

        public async Task<T?> UpdateByUserIdAsync(string userId, T entity)
        {
            // Get existing entity by userId and id
            var existing = await GetByUserIdAndKeyAsync(userId, entity.Id);

            // If entity does not exist, return null
            if (existing is null) return null;

            // Update existing entity with new values
            UpdateProperties(existing, entity);

            // Save changes in DB
            await SaveChangesAsync();
            return existing;
        }

        public async Task<List<T>> UpdateRangeByUserIdAsync(string userId, IEnumerable<T> updatedEntities)
        {
            // Get existing entities by userId and ids
            var existingEntities = await GetRangeByUserIdAsync(userId, updatedEntities.Select(e => e.Id));

            var updatedEntitiesDict = updatedEntities.ToDictionary(e => e.Id, e => e);
            // Update each existing entity with corresponding updated entity
            foreach (var existing in existingEntities)
                UpdateProperties(existing, updatedEntitiesDict[existing.Id]!);

            // Save changes in DB
            await SaveChangesAsync();
            return existingEntities;
        }

        public async Task<List<T>> DeleteAllByUserIdAsync(string userId)
        {
            // Get existing entities by userId
            var existing = await GetAllByUserIdAsync(userId);

            // Remove all entities with the specified userId
            return await RemoveEntityRangeAsync(existing);
        }

        public async Task<T?> DeleteByUserIdAndIdAsync(string userId, params object[] key)
        {
            // Get existing entity by userId and key
            var existing = await GetByUserIdAndKeyAsync(userId, key);

            // Remove entity if it exists
            return await RemoveEntityAsync(existing);
        }

        public async Task<List<T>> DeleteRangeByUserIdAndKeyAsync(string userId, IEnumerable<object[]> keys)
        {
            // Get existing entities by userId and keys
            var existingEntities = await GetRangeByUserIdAsync(userId, keys);

            // Remove range of entities if any exist
            return await RemoveEntityRangeAsync(existingEntities);
        }



        protected override void UpdateProperties(T existingEntity, T updatedEntity)
        {
            base.UpdateProperties(existingEntity, updatedEntity);
            // Ensure UserId is not modified during update
            _dbSet.Entry(existingEntity).Property(e => e.UserId).IsModified = false;
        }
    }
}