using Calibr8Fit.Api.Controllers.Abstract;
using Calibr8Fit.Api.Interfaces.Service;
using Calibr8Fit.Api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calibr8Fit.Api.Controllers
{
    [ApiController]
    [Route("api/friendship")]
    [Authorize]
    public class FriendshipController(
        ICurrentUserService currentUserService,
        IFriendshipService friendshipService,
        IPathService pathService
        ) : UserControllerBase(currentUserService)
    {
        private readonly IFriendshipService _friendshipService = friendshipService;
        private readonly IPathService _pathService = pathService;

        [HttpPost("request/{addresseeUsername}")]
        public Task<IActionResult> SendFriendRequest(string addresseeUsername) =>
            WithUserId(async userId =>
            {
                var result = await _friendshipService.SendFriendRequestAsync(userId, addresseeUsername);
                if (!result.Succeeded)
                    return BadRequest(new { errors = result.Errors });

                return Ok(result.Data!.ToFriendRequestDto(Request, _pathService));
            });

        [HttpPost("request/{requesterUsername}/accept")]
        public Task<IActionResult> AcceptFriendRequest(string requesterUsername) =>
            WithUserId(async userId =>
            {
                var result = await _friendshipService.AcceptFriendRequestAsync(userId, requesterUsername);
                if (!result.Succeeded)
                    return BadRequest(new { errors = result.Errors });

                return Ok(result.Data!.ToFriendshipDto(userId, Request, _pathService));
            });

        [HttpDelete("request/{requesterUsername}/reject")]
        public Task<IActionResult> RejectFriendRequest(string requesterUsername) =>
            WithUserId(async userId =>
            {
                var result = await _friendshipService.RejectFriendRequestAsync(userId, requesterUsername);
                if (!result.Succeeded)
                    return BadRequest(new { errors = result.Errors });

                return Ok(new { message = "Friend request rejected" });
            });

        [HttpDelete("request/{addresseeUsername}/cancel")]
        public Task<IActionResult> CancelFriendRequest(string addresseeUsername) =>
            WithUserId(async userId =>
            {
                var result = await _friendshipService.CancelFriendRequestAsync(userId, addresseeUsername);
                if (!result.Succeeded)
                    return BadRequest(new { errors = result.Errors });

                return Ok(new { message = "Friend request cancelled" });
            });

        [HttpGet("requests/pending")]
        public Task<IActionResult> GetPendingFriendRequests() =>
            WithUserId(async userId =>
            {
                var requests = await _friendshipService.GetPendingFriendRequestsAsync(userId);
                return Ok(requests.ToFriendRequestDtos(Request, _pathService));
            });

        [HttpGet("requests/sent")]
        public Task<IActionResult> GetSentFriendRequests() =>
            WithUserId(async userId =>
            {
                var requests = await _friendshipService.GetSentFriendRequestsAsync(userId);
                return Ok(requests.ToFriendRequestDtos(Request, _pathService));
            });

        // Friendship Management

        [HttpDelete("{friendUsername}")]
        public Task<IActionResult> RemoveFriendship(string friendUsername) =>
            WithUser(async user =>
            {
                var result = await _friendshipService.RemoveFriendshipAsync(user.UserName!, friendUsername);
                if (!result.Succeeded)
                    return BadRequest(new { errors = result.Errors });

                return Ok(new { message = "Friendship removed" });
            });

        [HttpGet("{username}/are-friends")]
        public Task<IActionResult> AreFriends(string username) =>
            WithUser(async user =>
            {
                var areFriends = await _friendshipService.AreFriendsAsync(user.UserName!, username);
                return Ok(new { areFriends });
            });

        [HttpGet("friends")]
        public Task<IActionResult> GetAllFriends() =>
            WithUserId(async userId =>
            {
                var friendships = await _friendshipService.GetUserFriendshipsAsync(userId);
                return Ok(friendships.ToFriendshipDtos(userId, Request, _pathService));
            });
    }
}