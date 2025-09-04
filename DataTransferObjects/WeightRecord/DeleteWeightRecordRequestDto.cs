using System.ComponentModel.DataAnnotations;

namespace Calibr8Fit.Api.DataTransferObjects.WeightRecord
{
    public class DeleteWeightRecordRequestDto
    {
        [Required]
        public required Guid Id { get; set; }
        [Required]
        public required DateTime DeletedAt { get; set; }
    }
}
