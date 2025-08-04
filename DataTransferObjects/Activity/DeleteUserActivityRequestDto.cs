using System.ComponentModel.DataAnnotations;

namespace Calibr8Fit.Api.DataTransferObjects.Activity
{
    public class DeleteUserActivityRequestDto
    {
        [Required]
        public required Guid Id { get; set; }
        [Required]
        public required DateTime DeletedAt { get; set; }
    }
}