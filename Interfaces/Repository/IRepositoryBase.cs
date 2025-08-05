using Calibr8Fit.Api.Interfaces.Model;

namespace Calibr8Fit.Api.Interfaces.Repository
{
    public interface IRepositoryBase<T> where T : class, IEntity
    {
        Task<T?> GetAsync(params object[] key);
        Task<List<T>> GetAllAsync();
        Task<bool> ExistsAsync(params object[] key);
        Task<T?> AddAsync(T entity);
        Task<List<T>> AddRangeAsync(IEnumerable<T> entities);
        Task<T?> UpdateAsync(T entity);
        Task<T> AddOrUpdateAsync(T entity);
        Task<List<T>> UpdateRangeAsync(IEnumerable<T> entities);
        Task<T?> DeleteAsync(params object[] key);
        Task<List<T>> DeleteRangeAsync(IEnumerable<object[]> keys);
        async Task<List<T>> DeleteRangeAsync(IEnumerable<object> keys)
        {
            var keyArrays = keys.Select(k => new object[] { k });
            return await DeleteRangeAsync(keyArrays);
        }
    }
}