namespace Calibr8Fit.Api.DataTransferObjects.ActivityRecord
{
    public class ActivityRecordDto
    {
        public required Guid Id { get; set; }
        public required Guid ActivityId { get; set; }
        public required int Duration { get; set; } // Duration in seconds
        public required float CaloriesBurned { get; set; }
        public required DateTime Time { get; set; }
        public required DateTime ModifiedAt { get; set; }
        public required bool Deleted { get; set; }
    }
}
