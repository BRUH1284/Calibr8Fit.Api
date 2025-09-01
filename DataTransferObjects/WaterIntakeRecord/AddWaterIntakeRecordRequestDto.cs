using System.ComponentModel.DataAnnotations;

namespace Calibr8Fit.Api.DataTransferObjects.WaterIntakeRecord
{
    public class AddWaterIntakeRecordRequestDto
    {
        public Guid Id { get; set; } // Optional, will be generated if not provided
        [Required]
        public required int AmountInMilliliters { get; set; }
        [Required]
        public required DateTime Time { get; set; }
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow; // Default to current time if not specified
        public bool Deleted { get; set; } = false; // Default to false if not specified
    }
}
