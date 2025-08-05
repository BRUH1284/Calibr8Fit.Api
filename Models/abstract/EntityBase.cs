using Calibr8Fit.Api.Interfaces.Model;

namespace Calibr8Fit.Api.Models
{
    public abstract class EntityBase<TKey> : IEntity
        where TKey : notnull
    {
        public virtual TKey? Id { get; set; }

        object[] IEntity.Id => [Id!];
    }
}