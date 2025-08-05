using System.ComponentModel.DataAnnotations;

namespace Calibr8Fit.Api.DataTransferObjects.Activity
{
    public class UpdateActivityRequestDto
    {
        [Required]
        public required Guid Id { get; set; }
        [Required]
        public required string MajorHeading { get; set; }
        [Required]
        public required float MetValue { get; set; }
        [Required]
        public required string Description { get; set; }
    }
}