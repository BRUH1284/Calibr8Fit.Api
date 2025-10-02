using Calibr8Fit.Api.DataTransferObjects.Post;
using Calibr8Fit.Api.Interfaces.Service;
using Calibr8Fit.Api.Models;

namespace Calibr8Fit.Api.Mappers
{
    public static class PostMapper
    {
        public static PostDto ToPostDto(
            this Post post,
            int likeCount,
            int commentCount,
            bool isLikedByCurrentUser,
            IPathService pathService
        ) => new PostDto
        {
            Id = post.Id,
            Author = post.User!.ToUserSummaryDto(pathService),
            Content = post.Content,
            CreatedAt = post.CreatedAt,
            CommentCount = commentCount,
            LikeCount = likeCount,
            IsLikedByCurrentUser = isLikedByCurrentUser,
            ImageUrls = post.Images!.Select(img =>
                pathService.GetPostImageUrl(
                    post.User!.UserName!,
                    post.Id,
                    img.Index
                )
            ).Where(url => url is not null)! // filter out nulls
        };

        public static CommentDto ToCommentDto(
            this Comment comment,
            IPathService pathService
        ) => new CommentDto
        {
            Id = comment.Id,
            Author = comment.User!.ToUserSummaryDto(pathService),
            Content = comment.Content,
            CreatedAt = comment.CreatedAt
        };

        public static IEnumerable<CommentDto> ToCommentDtos(
            this IEnumerable<Comment> comments,
            IPathService pathService
        ) => comments.Select(c => c.ToCommentDto(pathService));
    }
}
