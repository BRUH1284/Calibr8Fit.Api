using Calibr8Fit.Api.Enums;

namespace Calibr8Fit.Api.DataTransferObjects.User
{
    public class UpdateUserProfileSettingsRequestDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Gender? Gender { get; set; }
        public float? Weight { get; set; }
        public float? TargetWeight { get; set; }
        public float? Height { get; set; }
        public UserActivityLevel? ActivityLevel { get; set; }
        public UserClimate? Climate { get; set; }
    }
}