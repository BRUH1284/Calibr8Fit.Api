using Calibr8Fit.Api.DataTransferObjects.User;

namespace Calibr8Fit.Api.DataTransferObjects.Post
{
    public class PostDto
    {
        public required Guid Id { get; set; }
        public required UserSummaryDto Author { get; set; }
        public required string Content { get; set; }
        public required IEnumerable<string> ImageUrls { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CommentCount { get; set; }
        public int LikeCount { get; set; }
    }
}