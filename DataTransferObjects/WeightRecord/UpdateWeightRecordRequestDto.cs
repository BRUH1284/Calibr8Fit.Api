using System.ComponentModel.DataAnnotations;

namespace Calibr8Fit.Api.DataTransferObjects.WeightRecord
{
    public class UpdateWeightRecordRequestDto
    {
        [Required]
        public required Guid Id { get; set; }
        [Required]
        public required float Weight { get; set; } // Weight in kilograms
        [Required]
        public required DateTime Time { get; set; }
        [Required]
        public required DateTime ModifiedAt { get; set; }
        public required bool Deleted { get; set; } = false; // Default to false if not specified
    }
}
