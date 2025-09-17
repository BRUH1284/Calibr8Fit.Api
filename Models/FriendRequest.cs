using Calibr8Fit.Api.Interfaces.Model;

namespace Calibr8Fit.Api.Models
{
    public class FriendRequest : IEntity<string[]>
    {
        public required string RequesterId { get; set; }
        public required string AddresseeId { get; set; }

        public virtual User? Requester { get; set; }
        public virtual User? Addressee { get; set; }

        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;

        string[] IEntity<string[]>.Id => [RequesterId, AddresseeId];
    }
}