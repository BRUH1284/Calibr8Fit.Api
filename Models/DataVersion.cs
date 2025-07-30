using Calibr8Fit.Api.Enums;

namespace Calibr8Fit.Api.Models
{
    public class DataVersion
    {
        public required DataResource DataResource { get; set; }
        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;
    }
}