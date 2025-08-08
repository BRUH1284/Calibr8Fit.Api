using Calibr8Fit.Api.Interfaces.Model;

namespace Calibr8Fit.Api.Interfaces.Repository
{
    public interface IUserRepositoryBase<T, TKey> : IRepositoryBase<T, TKey>
        where T : class, IUserEntity<TKey>
        where TKey : notnull
    {
        Task<T?> GetByUserIdAndKeyAsync(string userId, TKey key);
        Task<List<T>> GetAllByUserIdAsync(string userId);
        Task<bool> UserKeyExistsAsync(string userId, TKey key);
        Task<T?> UpdateByUserIdAsync(string userId, T entity);
        Task<List<T>> UpdateRangeByUserIdAsync(string userId, IEnumerable<T> entities);
        Task<List<T>> DeleteAllByUserIdAsync(string userId);
        Task<T?> DeleteByUserIdAndIdAsync(string userId, TKey key);
        Task<List<T>> DeleteRangeByUserIdAndKeyAsync(string userId, IEnumerable<TKey> keys);
    }
}