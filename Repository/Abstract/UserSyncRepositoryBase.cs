using Calibr8Fit.Api.Data;
using Calibr8Fit.Api.Interfaces.Model;
using Calibr8Fit.Api.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Calibr8Fit.Api.Repository.Abstract
{
    public abstract class UserSyncRepositoryBase<T, HT, TKey>(
        ApplicationDbContext context
    ) : UserRepositoryBase<T, HT, TKey>(context), IUserSyncRepositoryBase<T, TKey>
        where T : class, ISyncableUserEntity<TKey>
        where HT : class, IEntity<TKey>
        where TKey : notnull
    {
        public virtual Task<DateTime> GetLastSyncedAtAsync(string userId)
        {
            return _dbSet
                .Where(ua => ua.UserId == userId)
                .Select(ua => ua.SyncedAt)
                .OrderByDescending(s => s)
                .FirstOrDefaultAsync();
        }
        public virtual Task<List<T>> GetAllFromDateByUserIdAsync(DateTime fromDate, string userId)
        {
            // Get all user activities for a specific user that have been synced after a certain date
            return _dbSet
                .Where(ua => ua.UserId == userId && ua.SyncedAt > fromDate)
                .ToListAsync();
        }
        public virtual async Task<T?> MarkAsDeletedByUserIdAsync(string userId, TKey key)
        {
            var existing = await _dbSet
                .FirstOrDefaultAsync(e => e.UserId == userId && e.Id.Equals(key) && !e.Deleted);

            // If entity does not exist, return null
            if (existing is null) return null;

            // Mark entity as deleted
            existing.Deleted = true;

            // Save changes in DB
            await SaveChangesAsync();
            return existing;
        }
        public virtual async Task<List<T>> MarkRangeAsDeletedByUserIdAsync(string userId, IEnumerable<TKey> keys)
        {
            var keySet = keys.ToHashSet();
            var entities = await _dbSet
                .Where(e => e.UserId == userId && keySet.Contains(e.Id) && !e.Deleted)
                .ToListAsync();

            // If no entities found, return empty list
            if (!entities.Any()) return [];

            // Mark entities as deleted
            foreach (var entity in entities)
                entity.Deleted = true;

            // Save changes in DB
            await SaveChangesAsync();
            return entities;
        }
    }

    public abstract class UserSyncRepositoryBase<T, TKey>(
        ApplicationDbContext context
    ) : UserSyncRepositoryBase<T, T, TKey>(context)
        where T : class, ISyncableUserEntity<TKey>
        where TKey : notnull
    {

    }
}