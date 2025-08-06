namespace Calibr8Fit.Api.Interfaces.Model
{
    public interface IEntity<TKey>
    {
        TKey Id { get; }
    }
}