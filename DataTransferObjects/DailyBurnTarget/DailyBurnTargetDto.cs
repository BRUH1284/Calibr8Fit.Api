namespace Calibr8Fit.Api.DataTransferObjects.DailyBurnTarget
{
    public class DailyBurnTargetDto
    {
        public required Guid Id { get; set; }
        public required Guid ActivityId { get; set; }
        public required int Duration { get; set; } // Duration in seconds
        public required DateTime ModifiedAt { get; set; }
        public required bool Deleted { get; set; }
    }
}