using Calibr8Fit.Api.DataTransferObjects.Friendship;
using Calibr8Fit.Api.Enums;
using Calibr8Fit.Api.Extensions;
using Calibr8Fit.Api.Interfaces.Repository;
using Calibr8Fit.Api.Interfaces.Repository.Base;
using Calibr8Fit.Api.Interfaces.Service;
using Calibr8Fit.Api.Mappers;
using Calibr8Fit.Api.Models;
using Calibr8Fit.Api.Services.Results;

namespace Calibr8Fit.Api.Services
{
    public class FriendshipService(
        IFriendshipRepository friendshipRepository,
        IRepositoryBase<FriendRequest, string[]> friendRequestRepository,
        IUserRepository userRepository,
        IPathService pathService,
        IPushService pushService) : IFriendshipService
    {
        private readonly IFriendshipRepository _friendshipRepository = friendshipRepository;
        private readonly IRepositoryBase<FriendRequest, string[]> _friendRequestRepository = friendRequestRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPathService _pathService = pathService;
        private readonly IPushService _pushService = pushService;

        // Helper method to get user ID from username
        private async Task<Result<string>> GetUserIdFromUsernameAsync(string username)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null)
                return Result<string>.Failure($"User with username '{username}' not found");

            return Result<string>.Success(user.Id);
        }

        public async Task<Result<FriendRequestDto>> SendFriendRequestAsync(string requesterId, string addresseeUsername)
        {
            // Get addressee ID from username
            var addresseeIdResult = await GetUserIdFromUsernameAsync(addresseeUsername);
            if (!addresseeIdResult.Succeeded)
                return Result<FriendRequestDto>.Failure(addresseeIdResult.Errors!.First());

            var addresseeId = addresseeIdResult.Data!;

            // Validate users are different
            if (requesterId == addresseeId)
                return Result<FriendRequestDto>.Failure("Cannot send friend request to yourself");

            // Check if they are already friends
            if (await _friendshipRepository.AreFriendsAsync(requesterId, addresseeId))
                return Result<FriendRequestDto>.Failure("Users are already friends");

            // Check if friend request already exists
            if (await _friendRequestRepository.KeyExistsAsync(requesterId, addresseeId))
                return Result<FriendRequestDto>.Failure("Friend request already sent");

            // Check if there's a reverse friend request (addressee sent to requester)
            if (await _friendRequestRepository.KeyExistsAsync(addresseeId, requesterId))
                return Result<FriendRequestDto>.Failure("This user has already sent you a friend request");

            // Create new friend request
            var friendRequest = new FriendRequest
            {
                RequesterId = requesterId,
                AddresseeId = addresseeId,
                RequestedAt = DateTime.UtcNow
            };

            var createdRequest = await _friendRequestRepository.AddAsync(friendRequest);
            if (createdRequest is null)
                return Result<FriendRequestDto>.Failure("Failed to create friend request");

            // Send notification to addressee
            var requester = (await _userRepository.GetAsync(requesterId))!;
            await _pushService.PushNotificationAsync(
                addresseeId,
                "New Friend Request",
                $"You have a new friend request from {requester.Profile!.FirstName} {requester.Profile!.LastName}",
                requester.GetProfilePictureUrl(_pathService)
                );

            return Result<FriendRequestDto>.Success(createdRequest.ToFriendRequestDto(_pathService));
        }

        public async Task<Result<FriendshipDto>> AcceptFriendRequestAsync(string addresseeId, string requesterUsername)
        {
            // Get requester ID from username
            var requesterIdResult = await GetUserIdFromUsernameAsync(requesterUsername);
            if (!requesterIdResult.Succeeded)
                return Result<FriendshipDto>.Failure(requesterIdResult.Errors!.First());

            var requesterId = requesterIdResult.Data!;

            // Get the friend request
            if (!await _friendRequestRepository.KeyExistsAsync(requesterId, addresseeId))
                return Result<FriendshipDto>.Failure("Friend request not found");

            // Create friendship
            var friendship = await _friendshipRepository.AddFriendshipAsync(requesterId, addresseeId);

            // Remove the friend request
            await _friendRequestRepository.DeleteAsync(requesterId, addresseeId);

            return Result<FriendshipDto>.Success(friendship.ToFriendshipDto(addresseeId, _pathService));
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

        public async Task<IEnumerable<FriendRequestDto>> GetPendingFriendRequestsAsync(string userId)
        {
            // Get all friend requests where the user is the addressee
            return (await _friendRequestRepository.QueryAsync(q => q.Where(fr => fr.AddresseeId == userId)))
                .Select(fr => fr.ToFriendRequestDto(_pathService));
        }

        // Get all friend requests where the user is the requester
        public async Task<IEnumerable<FriendRequestDto>> GetSentFriendRequestsAsync(string userId)
        {
            return (await _friendRequestRepository.QueryAsync(q => q.Where(fr => fr.RequesterId == userId)))
                .Select(fr => fr.ToFriendRequestDto(_pathService));
        }

        // Friendship Management
        public async Task<Result> RemoveFriendshipAsync(string userAUsername, string userBUsername)
        {
            // Get user IDs from usernames
            var userAIdResult = await GetUserIdFromUsernameAsync(userAUsername);
            if (!userAIdResult.Succeeded)
                return Result.Failure(userAIdResult.Errors!.First());

            var userBIdResult = await GetUserIdFromUsernameAsync(userBUsername);
            if (!userBIdResult.Succeeded)
                return Result.Failure(userBIdResult.Errors!.First());

            var userAId = userAIdResult.Data!;
            var userBId = userBIdResult.Data!;

            if (!await _friendshipRepository.RemoveFriendshipAsync(userAId, userBId))
                return Result.Failure("Friendship not found or already removed");

            return Result.Success();
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

        public async Task<IEnumerable<FriendshipDto>> GetUserFriendshipsAsync(string userId)
        {
            return (await _friendshipRepository.GetUserFriendshipsAsync(userId))
                .Select(f => f.ToFriendshipDto(userId, _pathService));
        }

        public async Task<int> GetFriendsCountAsync(string userId) =>
            await _friendshipRepository.GetFriendsCountAsync(userId);

        public async Task<FriendshipStatus> GetFriendshipStatusAsync(string currentUsername, string targetUsername)
        {
            // If it's the same user, return None
            if (currentUsername == targetUsername)
                return FriendshipStatus.None;

            // Get user IDs from usernames
            var currentUserIdResult = await GetUserIdFromUsernameAsync(currentUsername);
            var targetUserIdResult = await GetUserIdFromUsernameAsync(targetUsername);

            if (!currentUserIdResult.Succeeded || !targetUserIdResult.Succeeded)
                return FriendshipStatus.None;

            var currentUserId = currentUserIdResult.Data!;
            var targetUserId = targetUserIdResult.Data!;

            // Check if they are already friends
            if (await _friendshipRepository.AreFriendsAsync(currentUserId, targetUserId))
                return FriendshipStatus.Friends;

            // Check for pending friend requests
            // Check if current user sent request to target user
            if (await _friendRequestRepository.KeyExistsAsync(currentUserId, targetUserId))
                return FriendshipStatus.PendingSent;

            // Check if target user sent request to current user
            if (await _friendRequestRepository.KeyExistsAsync(targetUserId, currentUserId))
                return FriendshipStatus.PendingReceived;

            // No relationship
            return FriendshipStatus.None;
        }
    }
}