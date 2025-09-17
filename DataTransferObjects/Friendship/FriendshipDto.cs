using Calibr8Fit.Api.DataTransferObjects.User;

namespace Calibr8Fit.Api.DataTransferObjects.Friendship
{
    public class FriendshipDto
    {
        public required UserSummaryDto Friend { get; set; }
        public required DateTime FriendsSince { get; set; }
    }
}