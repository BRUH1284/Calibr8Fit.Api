using Calibr8Fit.Api.DataTransferObjects.Activity;
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
        Task<UserActivity?> UpdateByUserIdAsync(string userId, UpdateUserActivityRequestDto requestDto);
        Task<List<UserActivity>> UpdateRangeByUserIdAsync(string userId, IEnumerable<UpdateUserActivityRequestDto> requestDtos);
        Task<UserActivity?> UpdateAsync(UpdateUserActivityRequestDto requestDto);
        Task<UserActivity?> DeleteByUserIdAndIdAsync(string userId, Guid id);
        Task<List<UserActivity>> DeleteRangeByUserIdAndIdAsync(string userId, IEnumerable<Guid> ids);
        Task<UserActivity?> DeleteAsync(Guid id);
    }
}