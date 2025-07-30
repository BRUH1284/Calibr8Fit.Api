using Calibr8Fit.Api.Interfaces;

namespace Calibr8Fit.Api.Models
{
    public class Activity : IActivity
    {
        public int Code { get; set; }
        public required string MajorHeading { get; set; }
        public required float MetValue { get; set; }
        public required string Description { get; set; }
    }
}