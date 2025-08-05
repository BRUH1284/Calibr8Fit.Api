using Calibr8Fit.Api.Interfaces.Model;

namespace Calibr8Fit.Api.Interfaces.Service
{
    public interface ISyncService<T>
        where T : class, ISyncableUserEntity
    {
        Task<DateTime> GetLastSyncedAtAsync(string userId);
        Task<List<T>> Sync(string userId, List<T> entities, DateTime lastSyncedAt);
    }
}