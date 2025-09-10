using System.ComponentModel.DataAnnotations;

namespace Calibr8Fit.Api.DataTransferObjects.ConsumptionRecord
{
    public class AddConsumptionRecordRequestDto
    {
        public Guid Id { get; set; } // Optional, will be generated if not provided
        public Guid? FoodId { get; set; }
        public Guid? UserMealId { get; set; }
        [Required]
        public required float Quantity { get; set; } // Quantity in grams
        [Required]
        public required DateTime Time { get; set; }
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow; // Default to current time if not specified
        public bool Deleted { get; set; } = false; // Default to false if not specified
    }
}
