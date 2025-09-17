using Calibr8Fit.Api.Interfaces.Model;

namespace Calibr8Fit.Api.Models
{
    public class ProfilePicture : IUserEntity<string[]>
    {
        public required string UserId { get; set; }
        public required string FileName { get; set; }

        string[] IEntity<string[]>.Id => [UserId, FileName];
    }
}