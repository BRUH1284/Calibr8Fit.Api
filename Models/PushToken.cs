using Calibr8Fit.Api.Enums;
using Calibr8Fit.Api.Interfaces.Model;

namespace Calibr8Fit.Api.Models
{
    public class PushToken : IUserEntity<string[]>
    {
        public required string UserId { get; set; }
        public required string DeviceId { get; set; }
        public required string Token { get; set; }
        //public required Platform Platform { get; set; }

        string[] IEntity<string[]>.Id => [UserId, DeviceId];
    }
}