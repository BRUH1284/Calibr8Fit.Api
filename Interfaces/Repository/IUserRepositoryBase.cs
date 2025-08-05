using Calibr8Fit.Api.Interfaces.Model;

namespace Calibr8Fit.Api.Interfaces.Repository
{
    public interface IUserRepositoryBase<T> : IRepositoryBase<T>
        where T : class, IUserEntity
    {
        Task<T?> GetByUserIdAndKeyAsync(string userId, params object[] key);
        Task<List<T>> GetAllByUserIdAsync(string userId);
        Task<T?> UpdateByUserIdAsync(string userId, T entity);
        Task<List<T>> UpdateRangeByUserIdAsync(string userId, IEnumerable<T> entities);
        Task<List<T>> DeleteAllByUserIdAsync(string userId);
        Task<T?> DeleteByUserIdAndIdAsync(string userId, params object[] key);
        Task<List<T>> DeleteRangeByUserIdAndKeyAsync(string userId, IEnumerable<object[]> keys);
        async Task<List<T>> DeleteRangeByUserIdAndKeyAsync(string userId, IEnumerable<object> keys)
        {
            var keyArrays = keys.Select(k => new object[] { k });
            return await DeleteRangeByUserIdAndKeyAsync(userId, keyArrays);
        }
    }
}