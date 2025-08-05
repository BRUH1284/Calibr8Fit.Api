using System.ComponentModel.DataAnnotations.Schema;
using Calibr8Fit.Api.Enums;

namespace Calibr8Fit.Api.Models
{
    public class DataVersion : EntityBase<DataResource>
    {
        public required DataResource DataResource { get; set; }
        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;

        [NotMapped]
        public override DataResource Id
        {
            get => DataResource;
            set => DataResource = value!;
        }
    }
}