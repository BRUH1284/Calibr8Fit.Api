using System.ComponentModel.DataAnnotations;
using Calibr8Fit.Api.Interfaces.DataTransferObjects;

namespace Calibr8Fit.Api.DataTransferObjects.ConsumptionRecord
{
    public class UpdateConsumptionRecordRequestDto : IUpdateRequestDto<Guid>
    {
        [Required]
        public required Guid Id { get; set; }
        public Guid? FoodId { get; set; }
        public Guid? UserMealId { get; set; }
        [Required]
        public required float Quantity { get; set; } // Quantity in grams
        [Required]
        public required DateTime Time { get; set; }
        [Required]
        public required DateTime ModifiedAt { get; set; }
        public required bool Deleted { get; set; } = false; // Default to false if not specified
    }
}
