namespace Calibr8Fit.Api.DataTransferObjects.Activity
{
    public class UserActivityDto
    {
        public required Guid Id { get; set; }
        public required string MajorHeading { get; set; }
        public required float MetValue { get; set; }
        public required string Description { get; set; }
        public required DateTime UpdatedAt { get; set; }
    }
}