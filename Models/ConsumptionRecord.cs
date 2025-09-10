using Calibr8Fit.Api.Interfaces.Model;
using Calibr8Fit.Api.Models.Abstract;

namespace Calibr8Fit.Api.Models
{
    public class ConsumptionRecord : ISyncableUserEntity<Guid>
    {
        public Guid Id { get; set; }
        public required string UserId { get; set; }
        public virtual User? User { get; set; }

        public Guid? FoodId { get; set; }
        public virtual FoodBase? Food { get; set; }

        public Guid? UserMealId { get; set; }
        public virtual UserMeal? UserMeal { get; set; }

        public required float Quantity { get; set; } // Quantity in grams
        public required DateTime Time { get; set; }

        public DateTime SyncedAt { get; set; } = DateTime.UtcNow;
        public required DateTime ModifiedAt { get; set; }
        public bool Deleted { get; set; } = false;
    }
}