using Calibr8Fit.Api.Interfaces.Model;
using Calibr8Fit.Api.Models.Abstract;

namespace Calibr8Fit.Api.Models
{
    public class DailyBurnTarget : ISyncableUserEntity<Guid>
    {
        public Guid Id { get; set; }
        public required string UserId { get; set; }
        public virtual User? User { get; set; }
        public required Guid ActivityId { get; set; }
        public virtual ActivityBase? Activity { get; set; }
        public required int Duration { get; set; } // Duration in seconds

        public DateTime SyncedAt { get; set; } = DateTime.UtcNow;
        public required DateTime ModifiedAt { get; set; }
        public bool Deleted { get; set; } = false;
    }
}