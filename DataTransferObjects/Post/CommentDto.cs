using Calibr8Fit.Api.DataTransferObjects.User;

namespace Calibr8Fit.Api.DataTransferObjects.Post
{
    public class CommentDto
    {
        public required Guid Id { get; set; }
        public required UserSummaryDto Author { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}