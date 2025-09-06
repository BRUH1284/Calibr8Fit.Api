using System.ComponentModel.DataAnnotations;
using Calibr8Fit.Api.Interfaces.DataTransferObjects;

namespace Calibr8Fit.Api.DataTransferObjects.UserMeal
{
    public class UpdateUserMealRequestDto : IUpdateRequestDto<Guid>
    {
        [Required]
        public required Guid Id { get; set; }
        [Required]
        public required string Name { get; set; }
        public string? Notes { get; set; }
        public List<AddUserMealItemDto>? MealItems { get; set; } = [];
        [Required]
        public required DateTime ModifiedAt { get; set; }
        public required bool Deleted { get; set; } = false; // Default to false if not specified
    }
}
