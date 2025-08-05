using System.ComponentModel.DataAnnotations.Schema;

namespace Calibr8Fit.Api.Models
{
    public class RefreshToken : UserEntityBase<object[]>
    {
        public required string DeviceId { get; set; }
        public required string TokenHash { get; set; }
        public required DateTime ExpiresOn { get; set; }

        public bool IsActive => ExpiresOn > DateTime.UtcNow;

        [NotMapped]
        public override object[] Id => [UserId, DeviceId];
    }
}