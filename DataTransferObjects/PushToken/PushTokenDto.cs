using Calibr8Fit.Api.Enums;

namespace Calibr8Fit.Api.DataTransferObjects.PushToken
{
    public class PushTokenDto
    {
        public required string Token { get; set; }
        public required string DeviceId { get; set; }
        //public required Platform Platform { get; set; }
    }
}