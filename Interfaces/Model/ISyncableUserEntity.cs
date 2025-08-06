namespace Calibr8Fit.Api.Interfaces.Model
{
    public interface ISyncableUserEntity<TKey> : IUserEntity<TKey>
    {
        DateTime SyncedAt { get; set; }
        DateTime ModifiedAt { get; set; }
        bool Deleted { get; set; }
    }
}