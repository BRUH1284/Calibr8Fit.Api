using System.ComponentModel.DataAnnotations;
using Calibr8Fit.Api.Interfaces;

namespace Calibr8Fit.Api.DataTransferObjects.Activity
{
    public class ActivityDto
    {
        [Required]
        public required int Code { get; set; }
        [Required]
        public required string MajorHeading { get; set; }
        [Required]
        public required float MetValue { get; set; }
        [Required]
        public required string Description { get; set; }
    }
}