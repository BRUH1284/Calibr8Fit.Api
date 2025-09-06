using Calibr8Fit.Api.Interfaces.Model;

namespace Calibr8Fit.Api.Interfaces.Repository.Base
{
    public interface IDataVersionRepositoryBase<T, TKey> : IRepositoryBase<T, TKey>, IDataVersionProvider
        where T : class, IEntity<TKey>
        where TKey : notnull
    {

    }
}