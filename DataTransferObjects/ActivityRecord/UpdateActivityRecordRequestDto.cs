using System.ComponentModel.DataAnnotations;

namespace Calibr8Fit.Api.DataTransferObjects.ActivityRecord
{
    public class UpdateActivityRecordRequestDto
    {
        [Required]
        public required Guid Id { get; set; }
        [Required]
        public required int Duration { get; set; } // Duration in seconds
        [Required]
        public required float CaloriesBurned { get; set; }
        [Required]
        public required DateTime Time { get; set; }
        [Required]
        public required DateTime ModifiedAt { get; set; }
        public required bool Deleted { get; set; } = false; // Default to false if not specified
    }
}
