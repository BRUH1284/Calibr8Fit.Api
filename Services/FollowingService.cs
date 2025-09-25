using Calibr8Fit.Api.DataTransferObjects.User;
using Calibr8Fit.Api.Interfaces.Repository;
using Calibr8Fit.Api.Interfaces.Repository.Base;
using Calibr8Fit.Api.Interfaces.Service;
using Calibr8Fit.Api.Mappers;
using Calibr8Fit.Api.Models;
using Calibr8Fit.Api.Services.Results;

namespace Calibr8Fit.Api.Services
{
    public class FollowingService(
        IUserRepository userRepository,
        IUserRepositoryBase<UserFollower, (string, string)> userFollowerRepository,
        IPathService pathService
    ) : IFollowingService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IUserRepositoryBase<UserFollower, (string, string)> _userFollowerRepository = userFollowerRepository;
        private readonly IPathService _pathService = pathService;
        public async Task<Result> FollowUserAsync(string followerId, string followeeUsername)
        {
            var followeeId = (await _userRepository.GetByUsernameAsync(followeeUsername))?.Id;
            if (followeeId is null) return Result.Failure("User to follow not found");
            if (followerId == followeeId) return Result.Failure("Cannot follow yourself");

            var existingFollow = await _userFollowerRepository.GetAsync(followerId, followeeId);
            if (existingFollow is not null) return Result.Failure("Already following this user");

            var userFollower = new UserFollower
            {
                FollowerId = followerId,
                FolloweeId = followeeId
            };

            await _userFollowerRepository.AddAsync(userFollower);
            return Result.Success();
        }

        public async Task<Result> UnfollowUserAsync(string followerId, string followeeUsername)
        {
            var followeeId = (await _userRepository.GetByUsernameAsync(followeeUsername))?.Id;
            if (followeeId is null) return Result.Failure("User to unfollow not found");
            if (followerId == followeeId) return Result.Failure("Cannot unfollow yourself");

            var existingFollow = await _userFollowerRepository.GetAsync(followerId, followeeId);
            if (existingFollow is null) return Result.Failure("Not following this user");

            await _userFollowerRepository.DeleteAsync(existingFollow);
            return Result.Success();
        }

        public async Task<Result<List<UserSummaryDto>>> GetFollowersAsync(string userId)
        {
            var followers = await _userFollowerRepository.QueryAsync(f => f.Where(f => f.FolloweeId == userId));

            var followerDtos = followers.Select(f => f.Follower!.ToUserSummaryDto(_pathService)).ToList();

            return Result<List<UserSummaryDto>>.Success(followerDtos);
        }

        public async Task<int> GetFollowersCountAsync(string userId) =>
            await _userFollowerRepository.CountAsync(f => f.FolloweeId == userId);

        public async Task<Result<List<UserSummaryDto>>> GetFollowingAsync(string userId)
        {
            var following = await _userFollowerRepository.QueryAsync(f => f.Where(f => f.FollowerId == userId));

            var followingDtos = following.Select(f => f.Followee!.ToUserSummaryDto(_pathService)).ToList();

            return Result<List<UserSummaryDto>>.Success(followingDtos);
        }
        public async Task<int> GetFollowingCountAsync(string userId) =>
            await _userFollowerRepository.CountAsync(f => f.FollowerId == userId);
    }
}