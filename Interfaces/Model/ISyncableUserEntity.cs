namespace Calibr8Fit.Api.Interfaces.Model
{
    public interface ISyncableUserEntity : IUserEntity
    {
        DateTime SyncedAt { get; set; }
        DateTime ModifiedAt { get; set; }
        bool Deleted { get; set; }
    }
}