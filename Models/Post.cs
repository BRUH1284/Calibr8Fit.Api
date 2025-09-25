using Calibr8Fit.Api.Interfaces.Model;

namespace Calibr8Fit.Api.Models
{
    public class Post : IUserEntity<Guid>
    {
        public Guid Id { get; set; }
        public required string UserId { get; set; }
        public virtual User? User { get; set; }
        public required string Content { get; set; }
        public virtual ICollection<PostImage>? Images { get; set; } = [];
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public virtual ICollection<Comment>? Comments { get; set; } = [];
        public virtual ICollection<PostLike>? Likes { get; set; } = [];
    }
}