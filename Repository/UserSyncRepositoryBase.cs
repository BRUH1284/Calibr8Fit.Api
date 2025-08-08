using Calibr8Fit.Api.Data;
using Calibr8Fit.Api.Interfaces.Model;
using Calibr8Fit.Api.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Calibr8Fit.Api.Repository
{
    public abstract class UserSyncRepositoryBase<T, HT, TKey>(
        ApplicationDbContext context
    ) : UserRepositoryBase<T, HT, TKey>(context), IUserSyncRepositoryBase<T, TKey>
        where T : class, ISyncableUserEntity<TKey>
        where HT : class, IEntity<TKey>
        where TKey : notnull
    {
        public Task<DateTime> GetLastSyncedAtAsync(string userId)
        {
            return _dbSet
                .Where(ua => ua.UserId == userId)
                .Select(ua => ua.SyncedAt)
                .OrderByDescending(s => s)
                .FirstOrDefaultAsync();
        }
        public Task<List<T>> GetAllFromDateByUserIdAsync(DateTime fromDate, string userId)
        {
            // Get all user activities for a specific user that have been synced after a certain date
            return _dbSet
                .Where(ua => ua.UserId == userId && ua.SyncedAt > fromDate)
                .ToListAsync();
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