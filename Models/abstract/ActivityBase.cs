namespace Calibr8Fit.Api.Models
{
    public abstract class ActivityBase : EntityBase<Guid>
    {
        public required string MajorHeading { get; set; }
        public required float MetValue { get; set; }
        public required string Description { get; set; }
    }
}