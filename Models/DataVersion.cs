using Calibr8Fit.Api.Enums;
using Calibr8Fit.Api.Interfaces.Model;

namespace Calibr8Fit.Api.Models
{
    public class DataVersion : IEntity<DataResource>
    {
        public required DataResource DataResource { get; set; }
        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;

        DataResource IEntity<DataResource>.Id => DataResource;
    }
}