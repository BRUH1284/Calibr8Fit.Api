using Calibr8Fit.Api.Interfaces.Model;

namespace Calibr8Fit.Api.Interfaces.Repository
{
    public interface IUserSyncRepository<T> : IUserRepositoryBase<T>
        where T : class, IUserEntity
    {
        Task<DateTime> GetLastSyncedAtAsync(string userId);
        Task<List<T>> GetAllFromDateByUserIdAsync(DateTime fromDate, string userId);
    }
}