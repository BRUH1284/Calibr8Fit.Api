using Calibr8Fit.Api.DataTransferObjects.Friendship;
using Calibr8Fit.Api.Enums;
using Calibr8Fit.Api.Models;
using Calibr8Fit.Api.Services.Results;

namespace Calibr8Fit.Api.Interfaces.Service
{
    public interface IFriendshipService
    {
        // Friend Request Management
        Task<Result<FriendRequestDto>> SendFriendRequestAsync(string requesterId, string addresseeUsername);
        Task<Result<FriendshipDto>> AcceptFriendRequestAsync(string addresseeId, string requesterUsername);
        Task<Result> RejectFriendRequestAsync(string addresseeId, string requesterUsername);
        Task<Result> CancelFriendRequestAsync(string requesterId, string addresseeUsername);
        Task<IEnumerable<FriendRequestDto>> GetPendingFriendRequestsAsync(string userId);
        Task<IEnumerable<FriendRequestDto>> GetSentFriendRequestsAsync(string userId);

        // Friendship Management
        Task<Result> RemoveFriendshipAsync(string userAUsername, string userBUsername);
        Task<bool> AreFriendsAsync(string userAUsername, string userBUsername);
        Task<Friendship?> GetFriendshipAsync(string userAUsername, string userBUsername);
        Task<IEnumerable<User>> GetAllFriendsAsync(string userId);
        Task<IEnumerable<FriendshipDto>> GetUserFriendshipsAsync(string userId);
        Task<IEnumerable<FriendshipDto>> GetUserFriendshipsAsyncByUsername(string username);
        Task<IEnumerable<FriendshipDto>> SearchFriendshipsOfUserAsync(string userId, string query, int page, int size);
        Task<int> GetFriendsCountAsync(string userId);

        // Friendship Status
        Task<FriendshipStatus> GetFriendshipStatusAsync(string userId, string targetUsername);
    }
}