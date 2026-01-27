using Calibr8Fit.Api.Enums;

namespace Calibr8Fit.Api.DataTransferObjects.User
{
    public class UserProfileSettingsDto
    {
        public required string UserName { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required DateTime DateOfBirth { get; set; }
        public required Gender Gender { get; set; }
        public required float TargetWeight { get; set; }
        public required float Height { get; set; }
        public required UserActivityLevel ActivityLevel { get; set; }
        public required UserClimate Climate { get; set; }
        public required string? ProfilePictureUrl { get; set; }
    }
}