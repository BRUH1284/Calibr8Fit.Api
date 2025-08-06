using Calibr8Fit.Api.Interfaces.Model;

namespace Calibr8Fit.Api.Models
{
    public abstract class ActivityBase : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public required string MajorHeading { get; set; }
        public required float MetValue { get; set; }
        public required string Description { get; set; }

    }
}