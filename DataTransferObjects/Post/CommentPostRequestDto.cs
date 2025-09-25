namespace Calibr8Fit.Api.DataTransferObjects.Post
{
    public class CommentPostRequestDto
    {
        public required Guid PostId { get; set; }
        public required string Content { get; set; }
    }
}