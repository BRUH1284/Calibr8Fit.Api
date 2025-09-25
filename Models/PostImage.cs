using Calibr8Fit.Api.Interfaces.Model;

namespace Calibr8Fit.Api.Models
{
    public class PostImage : IEntity<(Guid, int)>
    {
        public required Guid PostId { get; set; }
        public required int Index { get; set; }

        (Guid, int) IEntity<(Guid, int)>.Id => (PostId, Index);
    }
}