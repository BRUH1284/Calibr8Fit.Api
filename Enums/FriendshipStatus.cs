namespace Calibr8Fit.Api.Enums
{
    public enum FriendshipStatus
    {
        None = 0,           // No relationship
        PendingSent = 1,    // Current user sent friend request
        PendingReceived = 2, // Current user received friend request
        Friends = 3         // Users are friends
    }
}