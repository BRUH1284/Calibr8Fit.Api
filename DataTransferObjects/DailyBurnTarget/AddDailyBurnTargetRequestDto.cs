using System.ComponentModel.DataAnnotations;

namespace Calibr8Fit.Api.DataTransferObjects.DailyBurnTarget
{
    public class AddDailyBurnTargetRequestDto
    {
        public Guid Id { get; set; } // Optional, will be generated if not provided
        [Required]
        public required Guid ActivityId { get; set; }
        [Required]
        public required int Duration { get; set; } // Duration in seconds
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow; // Default to current time if not specified
        public bool Deleted { get; set; } = false; // Default to false if not specified
    }
}