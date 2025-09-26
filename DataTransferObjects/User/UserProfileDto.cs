using Calibr8Fit.Api.Enums;

namespace Calibr8Fit.Api.DataTransferObjects.User
{
    public class UserProfileDto
    {
        public required string UserName { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required int FriendsCount { get; set; }
        public required int FollowersCount { get; set; }
        public required int FollowingCount { get; set; }
        public required string? Bio { get; set; }
        public required string? ProfilePictureUrl { get; set; }
        public required FriendshipStatus FriendshipStatus { get; set; }
        public required bool FollowedByMe { get; set; }
    }
}