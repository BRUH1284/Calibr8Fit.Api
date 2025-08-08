using System.ComponentModel.DataAnnotations;

namespace Calibr8Fit.Api.DataTransferObjects.ActivityRecord
{
    public class AddActivityRecordRequestDto
    {
        public Guid Id { get; set; } // Optional, will be generated if not provided
        [Required]
        public required Guid ActivityId { get; set; }
        [Required]
        public required int Duration { get; set; } // Duration in seconds
        [Required]
        public required float CaloriesBurned { get; set; }
        [Required]
        public required DateTime Time { get; set; }
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow; // Default to current time if not specified
        public bool Deleted { get; set; } = false; // Default to false if not specified
    }
}
