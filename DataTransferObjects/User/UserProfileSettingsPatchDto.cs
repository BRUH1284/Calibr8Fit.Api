using System.ComponentModel.DataAnnotations;
using Calibr8Fit.Api.Enums;

namespace Calibr8Fit.Api.DataTransferObjects.User
{
    public class UserProfileSettingsPatchDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Gender? Gender { get; set; }
        public float? TargetWeight { get; set; }
        public float? Height { get; set; }
        public UserActivityLevel? ActivityLevel { get; set; }
        public UserClimate? Climate { get; set; }
        public float? ForcedConsumptionTarget { get; set; }
        public float? ForcedHydrationTarget { get; set; }
        [Required]
        public required DateTime ModifiedAt { get; set; }
    }
}