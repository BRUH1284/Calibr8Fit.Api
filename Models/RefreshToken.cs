using Calibr8Fit.Api.Interfaces.Model;

namespace Calibr8Fit.Api.Models
{
    public class RefreshToken : IUserEntity<string[]>
    {
        public required string UserId { get; set; }
        public required string DeviceId { get; set; }
        public required string TokenHash { get; set; }
        public required DateTime ExpiresOn { get; set; }

        public bool IsActive => ExpiresOn > DateTime.UtcNow;

        string[] IEntity<string[]>.Id => [UserId, DeviceId];
    }
}