namespace Calibr8Fit.Api.Models
{
    public abstract class BaseActivity
    {
        public Guid Id { get; set; }
        public required string MajorHeading { get; set; }
        public required float MetValue { get; set; }
        public required string Description { get; set; }
    }
}