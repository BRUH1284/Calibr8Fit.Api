using Calibr8Fit.Api.Interfaces.Model;

namespace Calibr8Fit.Api.Models
{
    public class UserMeal : ISyncableUserEntity<Guid>
    {
        public Guid Id { get; set; }
        public required string UserId { get; set; }
        public virtual User? User { get; set; }

        public required string Name { get; set; }
        public string? Notes { get; set; }
        public required virtual ICollection<UserMealItem> MealItems { get; set; } = [];

        public DateTime SyncedAt { get; set; } = DateTime.UtcNow;
        public required DateTime ModifiedAt { get; set; }
        public bool Deleted { get; set; } = false;
    }
}