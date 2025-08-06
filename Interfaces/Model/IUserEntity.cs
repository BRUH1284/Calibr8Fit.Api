namespace Calibr8Fit.Api.Interfaces.Model
{
    public interface IUserEntity<TKey> : IEntity<TKey>
    {
        string UserId { get; }
    }
}