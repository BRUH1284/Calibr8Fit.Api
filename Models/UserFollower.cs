using Calibr8Fit.Api.Interfaces.Model;

namespace Calibr8Fit.Api.Models
{
    public class UserFollower : IUserEntity<(string, string)>
    {
        public required string FollowerId { get; set; }
        public virtual User? Follower { get; set; }
        public required string FolloweeId { get; set; }
        public virtual User? Followee { get; set; }
        public DateTime FollowedAt { get; set; } = DateTime.UtcNow;

        (string, string) IEntity<(string, string)>.Id => (FollowerId, FolloweeId);
        string IUserEntity<(string, string)>.UserId => FollowerId;
    }
}