using Calibr8Fit.Api.Interfaces.Model;

namespace Calibr8Fit.Api.Interfaces.Repository
{
    public interface IRepositoryBase<T, TKey>
        where T : class, IEntity<TKey>
        where TKey : notnull
    {
        Task<T?> GetAsync(TKey key);
        Task<List<T>> GetAllAsync();
        Task<bool> KeyExistsAsync(TKey key);
        Task<T?> AddAsync(T entity);
        Task<List<T>> AddRangeAsync(IEnumerable<T> entities);
        Task<T?> UpdateAsync(T entity);
        Task<T?> AddOrUpdateAsync(T entity);
        Task<List<T>> UpdateRangeAsync(IEnumerable<T> entities);
        Task<T?> DeleteAsync(TKey key);
        Task<List<T>> DeleteRangeAsync(IEnumerable<TKey> keys);
    }
}