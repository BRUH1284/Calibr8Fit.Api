using Calibr8Fit.Api.DataTransferObjects.User;

namespace Calibr8Fit.Api.DataTransferObjects.Friendship
{
    public class FriendRequestDto
    {
        public required UserSummaryDto Requester { get; set; }
        public required UserSummaryDto Receiver { get; set; }
        public required DateTime RequestedAt { get; set; }
    }
}