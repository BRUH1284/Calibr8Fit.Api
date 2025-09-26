using Calibr8Fit.Api.DataTransferObjects.User;
using Calibr8Fit.Api.Services.Results;

namespace Calibr8Fit.Api.Interfaces.Service
{
    public interface IFollowingService
    {
        Task<Result> FollowUserAsync(string followerId, string followeeUsername);
        Task<Result> UnfollowUserAsync(string followerId, string followeeUsername);
        Task<Result<List<UserSummaryDto>>> GetFollowersAsync(string userId);
        Task<int> GetFollowersCountAsync(string userId);
        Task<Result<List<UserSummaryDto>>> GetFollowingAsync(string userId);
        Task<int> GetFollowingCountAsync(string userId);
        Task<bool> IsFollowingAsync(string userId, string followeeUsername);
    }
}