using Calibr8Fit.Api.Models;

namespace Calibr8Fit.Api.Interfaces.Repository
{
    public interface IUserActivityRepository : IDataChecksumProvider
    {
        Task<UserActivity?> AddAsync(UserActivity userActivity);
        Task<List<UserActivity>> AddRangeAsync(IEnumerable<UserActivity> userActivities);
        Task<UserActivity?> GetByUserIdAndIdAsync(string userId, Guid id);
        Task<UserActivity?> GetByIdAsync(Guid id);
        Task<List<UserActivity>> GetAllByUserIdAsync(string userId);
        Task<UserActivity?> UpdateByUserIdAsync(string userId, UserActivity updateActivity);
        Task<List<UserActivity>> UpdateRangeByUserIdAsync(string userId, IEnumerable<UserActivity> updateActivities);
        Task<UserActivity?> UpdateAsync(UserActivity updateActivity);
        Task<UserActivity?> DeleteByUserIdAndIdAsync(string userId, Guid id, DateTime deletedAt);
        Task<List<UserActivity>> DeleteRangeByUserIdAndIdAsync(string userId, IEnumerable<(Guid id, DateTime deletedAt)> ids);
        Task<UserActivity?> DeleteAsync(Guid id, DateTime deletedAt);
    }
}