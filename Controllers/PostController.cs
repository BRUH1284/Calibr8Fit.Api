using Calibr8Fit.Api.Controllers.Abstract;
using Calibr8Fit.Api.DataTransferObjects.Post;
using Calibr8Fit.Api.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calibr8Fit.Api.Controllers
{
    [ApiController]
    [Route("api/post")]
    [Authorize]
    public class PostController(
        ICurrentUserService currentUserService,
        IPostService postService
        ) : UserControllerBase(currentUserService)
    {
        private readonly IPostService _postService = postService;

        [HttpPost]
        [Consumes("multipart/form-data")]
        public Task<IActionResult> CreatePost([FromForm] CreatePostRequestDto createPostRequestDto) =>
            WithUserId(async userId =>
            {
                var result = await _postService.CreatePostAsync(createPostRequestDto, userId);
                return result.Succeeded
                    ? CreatedAtAction(nameof(GetPost), new { postId = result.Data!.Id }, result.Data)
                    : BadRequest(new { errors = result.Errors });
            });

        [HttpGet("{postId}")]
        public async Task<IActionResult> GetPost(Guid postId)
        {
            var result = await _postService.GetPostAsync(postId);
            return result.Succeeded
                ? Ok(result.Data)
                : NotFound(new { errors = result.Errors });
        }

        [HttpGet("my")]
        public Task<IActionResult> GetMyPosts(
            [FromQuery] int page,
            [FromQuery] int size) =>
            WithUserId(async userId =>
            {
                var result = await _postService.GetLatestPostsByUserIdAsync(userId, page, size);
                return result.Succeeded
                    ? Ok(result.Data)
                    : NotFound(new { errors = result.Errors });
            });

        [HttpGet("user/{username}")]
        public async Task<IActionResult> GetPostsByUser(
            string username,
            [FromQuery] int page,
            [FromQuery] int size)
        {
            var result = await _postService.GetLatestPostsByUserNameAsync(username, page, size);
            return result.Succeeded
                ? Ok(result.Data)
                : NotFound(new { errors = result.Errors });
        }

    }
}