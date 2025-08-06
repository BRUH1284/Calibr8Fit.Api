using Calibr8Fit.Api.Interfaces.Model;

namespace Calibr8Fit.Api.Interfaces.Repository
{
    public interface IUserSyncRepository<T, TKey> : IUserRepositoryBase<T, TKey>
        where T : class, IUserEntity<TKey>
        where TKey : notnull
    {
        Task<DateTime> GetLastSyncedAtAsync(string userId);
        Task<List<T>> GetAllFromDateByUserIdAsync(DateTime fromDate, string userId);
    }
}