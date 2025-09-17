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
            HttpRequest httpRequest,
            IPathService pathService)
        {
            // Determine which user is the friend (not the current user)
            var friend = friendship.UserAId == currentUserId ? friendship.UserB : friendship.UserA;

            return new FriendshipDto
            {
                Friend = friend!.ToUserSummaryDto(friend!.GetProfilePictureUrl(httpRequest, pathService)),
                FriendsSince = friendship.FriendsSince
            };
        }

        public static FriendRequestDto ToFriendRequestDto(
            this FriendRequest friendRequest,
            HttpRequest httpRequest,
            IPathService pathService
            ) => new FriendRequestDto
            {
                Requester = friendRequest.Requester!.ToUserSummaryDto(friendRequest.Requester!.GetProfilePictureUrl(httpRequest, pathService)),
                Receiver = friendRequest.Addressee!.ToUserSummaryDto(friendRequest.Addressee!.GetProfilePictureUrl(httpRequest, pathService)),
                RequestedAt = friendRequest.RequestedAt
            };

        public static IEnumerable<FriendshipDto> ToFriendshipDtos(
            this IEnumerable<Friendship> friendships,
            string currentUserId,
            HttpRequest httpRequest,
            IPathService pathService
            ) => friendships.Select(f => f.ToFriendshipDto(currentUserId, httpRequest, pathService));

        public static IEnumerable<FriendRequestDto> ToFriendRequestDtos(
            this IEnumerable<FriendRequest> friendRequests,
            HttpRequest httpRequest,
            IPathService pathService
            ) => friendRequests.Select(fr => fr.ToFriendRequestDto(httpRequest, pathService));
    }
}