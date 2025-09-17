namespace Calibr8Fit.Api.Models
{
    public class Friendship
    {
        public required string UserAId { get; set; }
        public required string UserBId { get; set; }

        public virtual User? UserA { get; set; }
        public virtual User? UserB { get; set; }

        public DateTime FriendsSince { get; set; } = DateTime.UtcNow;
    }
}