using Calibr8Fit.Api.Enums;
using Calibr8Fit.Api.Interfaces.Model;

namespace Calibr8Fit.Api.Models
{
    public class UserProfile : ISyncableUserEntity<string>
    {
        public required string Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public DateTime DateOfBirth { get; set; } = DateTime.MinValue;
        public Gender Gender { get; set; } = Gender.Male;
        public float TargetWeight { get; set; } = 0.0f;
        public float Height { get; set; } = 0.0f;
        public UserActivityLevel ActivityLevel { get; set; } = UserActivityLevel.Sedentary;
        public UserClimate Climate { get; set; } = UserClimate.Temperate;
        public string? ProfilePictureFileName { get; set; }
        public float? ForcedConsumptionTarget { get; set; }
        public float? ForcedHydrationTarget { get; set; }
        public virtual ICollection<ProfilePicture>? ProfilePictures { get; set; }


        public DateTime SyncedAt { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedAt { get; set; }

        string IUserEntity<string>.UserId => Id;
        bool ISyncableUserEntity<string>.Deleted { get; set; } = false;
    }
}