using System.ComponentModel.DataAnnotations;

namespace Calibr8Fit.Api.DataTransferObjects.UserActivity
{
    public class UpdateUserActivityRequestDto
    {
        [Required]
        public required Guid Id { get; set; }
        [Required]
        public required string MajorHeading { get; set; }
        [Required]
        public required float MetValue { get; set; }
        [Required]
        public required string Description { get; set; }
        [Required]
        public required DateTime ModifiedAt { get; set; }
        public required bool Deleted { get; set; } = false; // Default to false if not specified
    }
}