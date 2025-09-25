using Calibr8Fit.Api.DataTransferObjects.Post;
using Calibr8Fit.Api.Services.Results;

namespace Calibr8Fit.Api.Interfaces.Service
{
    public interface IPostService
    {
        Task<Result<PostDto>> CreatePostAsync(CreatePostRequestDto createPostRequestDto, string userId);
        Task<Result<PostDto>> GetPostAsync(Guid postId);
        Task<Result<IEnumerable<PostDto>>> GetPostsByUserIdAsync(string userId);
        Task<Result<IEnumerable<PostDto>>> GetPostsByUserNameAsync(string username);
        Task<Result<IEnumerable<PostDto>>> GetLatestPostsByUserIdAsync(string userId, int page, int size);
        Task<Result<IEnumerable<PostDto>>> GetLatestPostsByUserNameAsync(string username, int page, int size);
        Task<Result> DeletePostAsync(Guid postId, string userId);
        Task<Result> LikePostAsync(Guid postId, string userId);
        Task<Result> UnlikePostAsync(Guid postId, string userId);
        Task<Result<CommentDto>> AddCommentAsync(Guid postId, string content, string userId);
        Task<Result> DeleteCommentAsync(Guid commentId, string userId);
    }
}