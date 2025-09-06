using Calibr8Fit.Api.Interfaces.Model;

namespace Calibr8Fit.Api.Interfaces.Repository.Base
{
    public interface IUserSyncRepositoryBase<T, TKey> : IUserRepositoryBase<T, TKey>
        where T : class, IUserEntity<TKey>
        where TKey : notnull
    {
        Task<DateTime> GetLastSyncedAtAsync(string userId);
        Task<List<T>> GetAllFromDateByUserIdAsync(DateTime fromDate, string userId);
        Task<T?> MarkAsDeletedByUserIdAsync(string userId, TKey key);
        Task<List<T>> MarkRangeAsDeletedByUserIdAsync(string userId, IEnumerable<TKey> keys);
    }
}