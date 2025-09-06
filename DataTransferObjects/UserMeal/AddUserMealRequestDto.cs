using System.ComponentModel.DataAnnotations;

namespace Calibr8Fit.Api.DataTransferObjects.UserMeal
{
    public class AddUserMealRequestDto
    {
        public Guid Id { get; set; } // Optional, will be generated if not provided
        [Required]
        public required string Name { get; set; }
        public string? Notes { get; set; }
        public List<AddUserMealItemDto>? MealItems { get; set; } = [];
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow; // Default to current time if not specified
        public bool Deleted { get; set; } = false; // Default to false if not specified
    }
}
