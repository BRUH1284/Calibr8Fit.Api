using Calibr8Fit.Api.DataTransferObjects.User;
using Calibr8Fit.Api.Enums;
using Calibr8Fit.Api.Extensions;
using Calibr8Fit.Api.Interfaces.Service;
using Calibr8Fit.Api.Models;

namespace Calibr8Fit.Api.Mappers
{
    public static class UserProfileMapper
    {
        public static UserSummaryDto ToUserSummaryDto(this User user, IPathService pathService)
        {
            return new UserSummaryDto
            {
                UserName = user.UserName!,
                FirstName = user.Profile!.FirstName,
                LastName = user.Profile!.LastName,
                ProfilePictureUrl = user.GetProfilePictureUrl(pathService)
            };
        }
        public static UserProfileSettingsDto ToUserProfileSettingsDto(this User user, IPathService pathService)
        {
            return new UserProfileSettingsDto
            {
                UserName = user.UserName!,
                FirstName = user.Profile!.FirstName,
                LastName = user.Profile!.LastName,
                DateOfBirth = user.Profile!.DateOfBirth,
                Gender = user.Profile!.Gender,
                Weight = user.Profile!.Weight,
                TargetWeight = user.Profile!.TargetWeight,
                Height = user.Profile!.Height,
                ActivityLevel = user.Profile!.ActivityLevel,
                Climate = user.Profile!.Climate,
                ProfilePictureUrl = user.GetProfilePictureUrl(pathService)
            };
        }
        public static UserProfileDto ToUserProfileDto(
            this User user,
            int friendsCount,
            int followersCount,
            int followingCount,
            FriendshipStatus friendshipStatus,
            bool followedByMe,
            IPathService pathService
            )
        {
            return new UserProfileDto
            {
                UserName = user.UserName!,
                FirstName = user.Profile!.FirstName,
                LastName = user.Profile!.LastName,
                ProfilePictureUrl = user.GetProfilePictureUrl(pathService),
                Bio = "",
                FriendsCount = friendsCount,
                FollowersCount = followersCount,
                FollowingCount = followingCount,
                FriendshipStatus = friendshipStatus,
                FollowedByMe = followedByMe
            };
        }
        //TODO: may break profile picture
        public static UserProfile ToUserProfile(this UpdateUserProfileSettingsRequestDto dto, User user)
        {
            return new UserProfile
            {
                Id = user.Id,
                FirstName = dto.FirstName ?? user.Profile!.FirstName,
                LastName = dto.LastName ?? user.Profile!.LastName,
                DateOfBirth = dto.DateOfBirth ?? user.Profile!.DateOfBirth,
                Gender = dto.Gender ?? user.Profile!.Gender,
                Weight = dto.Weight ?? user.Profile!.Weight,
                TargetWeight = dto.TargetWeight ?? user.Profile!.TargetWeight,
                Height = dto.Height ?? user.Profile!.Height,
                ActivityLevel = dto.ActivityLevel ?? user.Profile!.ActivityLevel,
                Climate = dto.Climate ?? user.Profile!.Climate
            };
        }
    }
}