namespace Calibr8Fit.Api.DataTransferObjects.ConsumptionRecord
{
    public class ConsumptionRecordDto
    {
        public required Guid Id { get; set; }
        public Guid? FoodId { get; set; }
        public Guid? UserMealId { get; set; }
        public required float Quantity { get; set; } // Quantity in grams
        public required DateTime Time { get; set; }
        public required DateTime ModifiedAt { get; set; }
        public required bool Deleted { get; set; }
    }
}
