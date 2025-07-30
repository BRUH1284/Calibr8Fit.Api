using System.ComponentModel.DataAnnotations;

namespace Calibr8Fit.Api.DataTransferObjects.Activity
{
    public class AddUserActivityRequestDto
    {
        [Required]
        public required string MajorHeading { get; set; }
        [Required]
        public required float MetValue { get; set; }
        [Required]
        public required string Description { get; set; }
        [Required]
        public required DateTime UpdatedAt { get; set; }
    }
}