using Calibr8Fit.Api.DataTransferObjects.Activity;
using Calibr8Fit.Api.Models;

namespace Calibr8Fit.Api.Interfaces.Repository
{
    public interface IUserActivityRepository : IDataChecksumProvider
    {
        Task<UserActivity> AddAsync(UserActivity userActivity);
        Task<UserActivity?> GetByUserIdAndIdAsync(string userId, Guid id);
        Task<UserActivity?> GetByIdAsync(Guid id);
        Task<List<UserActivity>> GetAllByUserIdAsync(string userId);
        Task<UserActivity?> UpdateByUserIdAndIdAsync(string userId, Guid id, UpdateActivityRequestDto requestDto);
        Task<UserActivity?> UpdateAsync(Guid id, UpdateActivityRequestDto requestDto);
        Task<UserActivity?> DeleteByUserIdAndIdAsync(string userId, Guid id);
        Task<UserActivity?> DeleteAsync(Guid id);
    }
}