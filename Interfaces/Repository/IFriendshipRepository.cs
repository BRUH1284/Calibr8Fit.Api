using Calibr8Fit.Api.Models;

namespace Calibr8Fit.Api.Interfaces.Repository
{
    public interface IFriendshipRepository
    {
        Task<Friendship?> GetFriendshipAsync(string userAId, string userBId);
        Task<bool> AreFriendsAsync(string userAId, string userBId);
        Task<IEnumerable<User>> GetAllFriendsAsync(string userId);
        Task<int> GetFriendsCountAsync(string userId);
        Task<IEnumerable<Friendship>> GetUserFriendshipsAsync(string userId);
        Task<IEnumerable<Friendship>> SearchFriendshipsOfUserAsync(string userId, string query, int page, int size);
        Task<Friendship> AddFriendshipAsync(string userAId, string userBId);
        Task<bool> RemoveFriendshipAsync(string userAId, string userBId);
        Task<bool> ExistsAsync(string userAId, string userBId);
    }
}