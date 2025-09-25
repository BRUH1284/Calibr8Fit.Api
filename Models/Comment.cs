using Calibr8Fit.Api.Interfaces.Model;

namespace Calibr8Fit.Api.Models
{
    public class Comment : IUserEntity<Guid>
    {
        public Guid Id { get; set; }
        public required string UserId { get; set; }
        public virtual User? User { get; set; }
        public required Guid PostId { get; set; }
        public virtual Post? Post { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}