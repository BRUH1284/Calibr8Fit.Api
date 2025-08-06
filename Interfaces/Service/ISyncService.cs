using Calibr8Fit.Api.Interfaces.Model;

namespace Calibr8Fit.Api.Interfaces.Service
{
    public interface ISyncService<T, TKey>
        where T : class, ISyncableUserEntity<TKey>
        where TKey : notnull
    {
        Task<DateTime> GetLastSyncedAtAsync(string userId);
        Task<List<T>> Sync(string userId, IEnumerable<T> entities, DateTime lastSyncedAt);
    }
}