using System.ComponentModel.DataAnnotations;

namespace Calibr8Fit.Api.DataTransferObjects.WaterIntakeRecord
{
    public class DeleteWaterIntakeRecordRequestDto
    {
        [Required]
        public required Guid Id { get; set; }
        [Required]
        public required DateTime DeletedAt { get; set; }
    }
}
