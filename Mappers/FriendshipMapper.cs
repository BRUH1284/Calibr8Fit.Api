using Calibr8Fit.Api.DataTransferObjects.Friendship;
using Calibr8Fit.Api.Extensions;
using Calibr8Fit.Api.Interfaces.Service;
using Calibr8Fit.Api.Models;

namespace Calibr8Fit.Api.Mappers
{
    public static class FriendshipMapper
    {
        public static FriendshipDto ToFriendshipDto(
            this Friendship friendship,
            string currentUserId,
            IPathService pathService
            )
        {
            // Determine which user is the friend (not the current user)
            var friend = friendship.UserAId == currentUserId ? friendship.UserB : friendship.UserA;

            return new FriendshipDto
            {
                Friend = friend!.ToUserSummaryDto(friend!.GetProfilePictureUrl(pathService)),
                FriendsSince = friendship.FriendsSince
            };
        }

        public static FriendRequestDto ToFriendRequestDto(
            this FriendRequest friendRequest,
            IPathService pathService
            ) => new FriendRequestDto
            {
                Requester = friendRequest.Requester!.ToUserSummaryDto(friendRequest.Requester!.GetProfilePictureUrl(pathService)),
                Receiver = friendRequest.Addressee!.ToUserSummaryDto(friendRequest.Addressee!.GetProfilePictureUrl(pathService)),
                RequestedAt = friendRequest.RequestedAt
            };

        public static IEnumerable<FriendshipDto> ToFriendshipDtos(
            this IEnumerable<Friendship> friendships,
            string currentUserId,
            IPathService pathService
            ) => friendships.Select(f => f.ToFriendshipDto(currentUserId, pathService));

        public static IEnumerable<FriendRequestDto> ToFriendRequestDtos(
            this IEnumerable<FriendRequest> friendRequests,
            IPathService pathService
            ) => friendRequests.Select(fr => fr.ToFriendRequestDto(pathService));
    }
}