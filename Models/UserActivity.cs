using Calibr8Fit.Api.Interfaces.Model;

namespace Calibr8Fit.Api.Models
{
    public class UserActivity : ActivityBase, ISyncableUserEntity
    {
        public required string UserId { get; set; }
        public virtual User? User { get; set; }

        public DateTime SyncedAt { get; set; } = DateTime.UtcNow;
        public required DateTime ModifiedAt { get; set; }
        public bool Deleted { get; set; } = false;
    }
}