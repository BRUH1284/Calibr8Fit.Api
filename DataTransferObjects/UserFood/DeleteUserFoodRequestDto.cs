using System.ComponentModel.DataAnnotations;

namespace Calibr8Fit.Api.DataTransferObjects.UserFood
{
    public class DeleteUserFoodRequestDto
    {
        [Required]
        public required Guid Id { get; set; }
        [Required]
        public required DateTime DeletedAt { get; set; }
    }
}
