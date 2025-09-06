using System.ComponentModel.DataAnnotations;

namespace Calibr8Fit.Api.DataTransferObjects.UserMeal
{
    public class DeleteUserMealRequestDto
    {
        [Required]
        public required Guid Id { get; set; }
        [Required]
        public required DateTime DeletedAt { get; set; }
    }
}
