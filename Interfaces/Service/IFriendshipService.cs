using Calibr8Fit.Api.Models;
using Calibr8Fit.Api.Services.Results;

namespace Calibr8Fit.Api.Interfaces.Service
{
    public interface IFriendshipService
    {
        // Friend Request Management
        Task<Result<FriendRequest>> SendFriendRequestAsync(string requesterId, string addresseeUsername);
        Task<Result<Friendship>> AcceptFriendRequestAsync(string addresseeId, string requesterUsername);
        Task<Result> RejectFriendRequestAsync(string addresseeId, string requesterUsername);
        Task<Result> CancelFriendRequestAsync(string requesterId, string addresseeUsername);
        Task<IEnumerable<FriendRequest>> GetPendingFriendRequestsAsync(string userId);
        Task<IEnumerable<FriendRequest>> GetSentFriendRequestsAsync(string userId);

        // Friendship Management
        Task<Result<Friendship?>> RemoveFriendshipAsync(string userAUsername, string userBUsername);
        Task<bool> AreFriendsAsync(string userAUsername, string userBUsername);
        Task<Friendship?> GetFriendshipAsync(string userAUsername, string userBUsername);
        Task<IEnumerable<User>> GetAllFriendsAsync(string userId);
        Task<IEnumerable<Friendship>> GetUserFriendshipsAsync(string userId);
    }
}