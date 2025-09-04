namespace Calibr8Fit.Api.DataTransferObjects.WeightRecord
{
    public class WeightRecordDto
    {
        public required Guid Id { get; set; }
        public required float Weight { get; set; } // Weight in kilograms
        public required DateTime Time { get; set; }
        public required DateTime ModifiedAt { get; set; }
        public required bool Deleted { get; set; }
    }
}
