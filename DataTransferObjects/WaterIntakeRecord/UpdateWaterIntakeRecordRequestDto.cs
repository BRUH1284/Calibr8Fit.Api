using System.ComponentModel.DataAnnotations;

namespace Calibr8Fit.Api.DataTransferObjects.WaterIntakeRecord
{
    public class UpdateWaterIntakeRecordRequestDto
    {
        [Required]
        public required Guid Id { get; set; }
        [Required]
        public required int AmountInMilliliters { get; set; }
        [Required]
        public required DateTime Time { get; set; }
        [Required]
        public required DateTime ModifiedAt { get; set; }
        public required bool Deleted { get; set; } = false; // Default to false if not specified
    }
}
