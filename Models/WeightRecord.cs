using Calibr8Fit.Api.Interfaces.Model;

namespace Calibr8Fit.Api.Models
{
    public class WeightRecord : ISyncableUserEntity<Guid>
    {
        public Guid Id { get; set; }
        public required string UserId { get; set; }
        public virtual User? User { get; set; }

        public required float Weight { get; set; } // Weight in kilograms
        public required DateTime Time { get; set; }

        public DateTime SyncedAt { get; set; } = DateTime.UtcNow;
        public required DateTime ModifiedAt { get; set; }
        public bool Deleted { get; set; } = false;
    }
}