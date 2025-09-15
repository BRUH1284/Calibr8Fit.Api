using System.ComponentModel.DataAnnotations;
using Calibr8Fit.Api.Interfaces.DataTransferObjects;

namespace Calibr8Fit.Api.DataTransferObjects.DailyBurnTarget
{
    public class UpdateDailyBurnTargetRequestDto : IUpdateRequestDto<Guid>
    {
        [Required]
        public required Guid Id { get; set; }
        [Required]
        public required Guid ActivityId { get; set; }
        [Required]
        public required int Duration { get; set; } // Duration in seconds
        [Required]
        public required DateTime ModifiedAt { get; set; }
        public bool Deleted { get; set; } = false; // Default to false if not specified
    }
}