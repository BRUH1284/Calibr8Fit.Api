using Calibr8Fit.Api.Interfaces;

namespace Calibr8Fit.Api.Models
{
    public class UserActivity : IActivity
    {
        public Guid Id { get; set; }
        public required string UserId { get; set; }
        public virtual User? User { get; set; }

        public required string MajorHeading { get; set; }
        public required float MetValue { get; set; }
        public required string Description { get; set; }
    }
}