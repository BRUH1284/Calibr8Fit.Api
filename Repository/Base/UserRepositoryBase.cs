using Calibr8Fit.Api.Data;
using Calibr8Fit.Api.Interfaces.Model;
using Calibr8Fit.Api.Interfaces.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace Calibr8Fit.Api.Repository.Base
{
    public class UserRepositoryBase<T, HT, TKey>(
        ApplicationDbContext context
        ) : RepositoryBase<T, HT, TKey>(context), IUserRepositoryBase<T, TKey>
        where T : class, IUserEntity<TKey>
        where HT : class, IEntity<TKey>
        where TKey : notnull
    {
        public virtual Task<List<T>> GetAllByUserIdAsync(string userId)
        {
            // Get all entities by userId
            return _dbSet.Where(e => e.UserId.Equals(userId))
                .ToListAsync();
        }

        public virtual Task<T?> GetByUserIdAndKeyAsync(string userId, TKey key)
        {
            // Get entity by userId and key
            return _dbSet
                .FirstOrDefaultAsync(e => e.UserId.Equals(userId) && e.Id.Equals(key));
        }

        public virtual Task<List<T>> GetRangeByUserIdAsync(string userId, IEnumerable<TKey> keys)
        {
            var keySet = keys.ToHashSet();
            // Get range of entities by userId and keys
            return _dbSet
                .Where(e => e.UserId.Equals(userId) && keySet.Contains(e.Id))
                .ToListAsync();
        }
        public virtual Task<bool> UserKeyExistsAsync(string userId, TKey key)
        {
            // Check if entity exists by userId and key
            return _dbSet.AnyAsync(e => e.Id.Equals(key) && e.UserId.Equals(userId));
        }

        public virtual async Task<T?> UpdateByUserIdAsync(string userId, T entity)
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

        public virtual async Task<List<T>> UpdateRangeByUserIdAsync(string userId, IEnumerable<T> updatedEntities)
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

        public virtual async Task<List<T>> DeleteAllByUserIdAsync(string userId)
        {
            // Get existing entities by userId
            var existing = await GetAllByUserIdAsync(userId);

            // Remove all entities with the specified userId
            return await RemoveEntityRangeAsync(existing);
        }

        public virtual async Task<T?> DeleteByUserIdAndIdAsync(string userId, TKey key)
        {
            // Get existing entity by userId and key
            var existing = await GetByUserIdAndKeyAsync(userId, key);

            // Remove entity if it exists
            return await RemoveEntityAsync(existing);
        }

        public virtual async Task<List<T>> DeleteRangeByUserIdAndKeyAsync(string userId, IEnumerable<TKey> keys)
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

    public class UserRepositoryBase<T, TKey>(
        ApplicationDbContext context
        ) : UserRepositoryBase<T, T, TKey>(context)
        where T : class, IUserEntity<TKey>
        where TKey : notnull
    {

    }
}