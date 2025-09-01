namespace Calibr8Fit.Api.DataTransferObjects.WaterIntakeRecord
{
    public class WaterIntakeRecordDto
    {
        public required Guid Id { get; set; }
        public required int AmountInMilliliters { get; set; }
        public required DateTime Time { get; set; }
        public required DateTime ModifiedAt { get; set; }
        public required bool Deleted { get; set; }
    }
}
