using Calibr8Fit.Api.Enums;

namespace Calibr8Fit.Api.Models
{
    public class UserProfile
    {
        public required string UserId { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public DateTime DateOfBirth { get; set; } = DateTime.MinValue;
        public Gender Gender { get; set; } = Gender.Male;
        public float Weight { get; set; } = 0.0f;
        public float TargetWeight { get; set; } = 0.0f;
        public float Height { get; set; } = 0.0f;
        public UserActivityLevel ActivityLevel { get; set; } = UserActivityLevel.Sedentary;
        public UserGoal Goal { get; set; } = UserGoal.MaintainWeight;
        public UserClimate Climate { get; set; } = UserClimate.Temperate;
    }
}