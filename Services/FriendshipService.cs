using Calibr8Fit.Api.Interfaces.Repository;
using Calibr8Fit.Api.Interfaces.Repository.Base;
using Calibr8Fit.Api.Interfaces.Service;
using Calibr8Fit.Api.Models;
using Calibr8Fit.Api.Services.Results;

namespace Calibr8Fit.Api.Services
{
    public class FriendshipService(
        IFriendshipRepository friendshipRepository,
        IRepositoryBase<FriendRequest, string[]> friendRequestRepository,
        IUserRepository userRepository) : IFriendshipService
    {
        private readonly IFriendshipRepository _friendshipRepository = friendshipRepository;
        private readonly IRepositoryBase<FriendRequest, string[]> _friendRequestRepository = friendRequestRepository;
        private readonly IUserRepository _userRepository = userRepository;

        // Helper method to get user ID from username
        private async Task<Result<string>> GetUserIdFromUsernameAsync(string username)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null)
                return Result<string>.Failure($"User with username '{username}' not found");

            return Result<string>.Success(user.Id);
        }

        public async Task<Result<FriendRequest>> SendFriendRequestAsync(string requesterId, string addresseeUsername)
        {
            // Get addressee ID from username
            var addresseeIdResult = await GetUserIdFromUsernameAsync(addresseeUsername);
            if (!addresseeIdResult.Succeeded)
                return Result<FriendRequest>.Failure(addresseeIdResult.Errors!.First());

            var addresseeId = addresseeIdResult.Data!;

            // Validate users are different
            if (requesterId == addresseeId)
                return Result<FriendRequest>.Failure("Cannot send friend request to yourself");

            // Check if they are already friends
            if (await _friendshipRepository.AreFriendsAsync(requesterId, addresseeId))
                return Result<FriendRequest>.Failure("Users are already friends");

            // Check if friend request already exists
            if (await _friendRequestRepository.KeyExistsAsync(requesterId, addresseeId))
                return Result<FriendRequest>.Failure("Friend request already sent");

            // Check if there's a reverse friend request (addressee sent to requester)
            if (await _friendRequestRepository.KeyExistsAsync(addresseeId, requesterId))
                return Result<FriendRequest>.Failure("This user has already sent you a friend request");

            // Create new friend request
            var friendRequest = new FriendRequest
            {
                RequesterId = requesterId,
                AddresseeId = addresseeId,
                RequestedAt = DateTime.UtcNow
            };

            var createdRequest = await _friendRequestRepository.AddAsync(friendRequest);
            if (createdRequest is null)
                return Result<FriendRequest>.Failure("Failed to create friend request");

            return Result<FriendRequest>.Success(createdRequest);
        }

        public async Task<Result<Friendship>> AcceptFriendRequestAsync(string addresseeId, string requesterUsername)
        {
            // Get requester ID from username
            var requesterIdResult = await GetUserIdFromUsernameAsync(requesterUsername);
            if (!requesterIdResult.Succeeded)
                return Result<Friendship>.Failure(requesterIdResult.Errors!.First());

            var requesterId = requesterIdResult.Data!;

            // Get the friend request
            if (!await _friendRequestRepository.KeyExistsAsync(requesterId, addresseeId))
                return Result<Friendship>.Failure("Friend request not found");

            // Create friendship
            var friendship = await _friendshipRepository.AddFriendshipAsync(requesterId, addresseeId);

            // Remove the friend request
            await _friendRequestRepository.DeleteAsync(requesterId, addresseeId);

            return Result<Friendship>.Success(friendship);
        }

        public async Task<Result> RejectFriendRequestAsync(string addresseeId, string requesterUsername)
        {
            // Get requester ID from username
            var requesterIdResult = await GetUserIdFromUsernameAsync(requesterUsername);
            if (!requesterIdResult.Succeeded)
                return Result.Failure(requesterIdResult.Errors!.First());

            var requesterId = requesterIdResult.Data!;

            // Get the friend request
            if (!await _friendRequestRepository.KeyExistsAsync(requesterId, addresseeId))
                return Result.Failure("Friend request not found");

            // Remove the friend request
            await _friendRequestRepository.DeleteAsync(requesterId, addresseeId);

            return Result.Success();
        }

        public async Task<Result> CancelFriendRequestAsync(string requesterId, string addresseeUsername)
        {
            // Get addressee ID from username
            var addresseeIdResult = await GetUserIdFromUsernameAsync(addresseeUsername);
            if (!addresseeIdResult.Succeeded)
                return Result.Failure(addresseeIdResult.Errors!.First());

            var addresseeId = addresseeIdResult.Data!;

            // Get the friend request
            if (!await _friendRequestRepository.KeyExistsAsync(requesterId, addresseeId))
                return Result.Failure("Friend request not found");

            // Remove the friend request (only the requester can cancel)
            await _friendRequestRepository.DeleteAsync(requesterId, addresseeId);

            return Result.Success();
        }

        public async Task<IEnumerable<FriendRequest>> GetPendingFriendRequestsAsync(string userId)
        {
            // Get all friend requests where the user is the addressee
            return await _friendRequestRepository.QueryAsync(q => q.Where(fr => fr.AddresseeId == userId));
        }

        // Get all friend requests where the user is the requester
        public async Task<IEnumerable<FriendRequest>> GetSentFriendRequestsAsync(string userId)
        {
            return await _friendRequestRepository.QueryAsync(q => q.Where(fr => fr.RequesterId == userId));
        }

        // Friendship Management
        public async Task<Result<Friendship?>> RemoveFriendshipAsync(string userAUsername, string userBUsername)
        {
            // Get user IDs from usernames
            var userAIdResult = await GetUserIdFromUsernameAsync(userAUsername);
            if (!userAIdResult.Succeeded)
                return Result<Friendship?>.Failure(userAIdResult.Errors!.First());

            var userBIdResult = await GetUserIdFromUsernameAsync(userBUsername);
            if (!userBIdResult.Succeeded)
                return Result<Friendship?>.Failure(userBIdResult.Errors!.First());

            var userAId = userAIdResult.Data!;
            var userBId = userBIdResult.Data!;

            if (!await _friendshipRepository.RemoveFriendshipAsync(userAId, userBId))
                return Result<Friendship?>.Failure("Friendship not found or already removed");

            return Result<Friendship?>.Success(null);
        }

        public async Task<bool> AreFriendsAsync(string userAUsername, string userBUsername)
        {
            // Get user IDs from usernames
            var userAIdResult = await GetUserIdFromUsernameAsync(userAUsername);
            if (!userAIdResult.Succeeded)
                return false;

            var userBIdResult = await GetUserIdFromUsernameAsync(userBUsername);
            if (!userBIdResult.Succeeded)
                return false;

            return await _friendshipRepository.AreFriendsAsync(userAIdResult.Data!, userBIdResult.Data!);
        }

        public async Task<Friendship?> GetFriendshipAsync(string userAUsername, string userBUsername)
        {
            // Get user IDs from usernames
            var userAIdResult = await GetUserIdFromUsernameAsync(userAUsername);
            if (!userAIdResult.Succeeded)
                return null;

            var userBIdResult = await GetUserIdFromUsernameAsync(userBUsername);
            if (!userBIdResult.Succeeded)
                return null;

            return await _friendshipRepository.GetFriendshipAsync(userAIdResult.Data!, userBIdResult.Data!);
        }

        public async Task<IEnumerable<User>> GetAllFriendsAsync(string userId)
        {
            return await _friendshipRepository.GetAllFriendsAsync(userId);
        }

        public async Task<IEnumerable<Friendship>> GetUserFriendshipsAsync(string userId)
        {
            return await _friendshipRepository.GetUserFriendshipsAsync(userId);
        }
    }
}