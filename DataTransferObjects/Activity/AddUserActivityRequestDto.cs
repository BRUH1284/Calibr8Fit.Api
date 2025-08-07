using System.ComponentModel.DataAnnotations;

namespace Calibr8Fit.Api.DataTransferObjects.Activity
{
    public class AddUserActivityRequestDto
    {
        public Guid Id { get; set; } // Optional, will be generated if not provided
        [Required]
        public required string MajorHeading { get; set; }
        [Required]
        public required float MetValue { get; set; }
        [Required]
        public required string Description { get; set; }
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow; // Default to current time if not specified
        public bool Deleted { get; set; } = false; // Default to false if not specified
    }
}