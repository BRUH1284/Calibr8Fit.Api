using System.ComponentModel.DataAnnotations;

namespace Calibr8Fit.Api.DataTransferObjects.Token
{
    public class TokenRequestDto
    {
        [Required]
        public required string OldAccessToken { get; set; }
        [Required]
        public required string RefreshToken { get; set; }
        [Required]
        public required string DeviceId { get; set; }
    }
}