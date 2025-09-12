using Calibr8Fit.Api.Interfaces.Model;

namespace Calibr8Fit.Api.Interfaces.Service
{
    public interface ITPHValidationService<
        TKey,
        TEntity,
        TUserEntity
    >
        where TEntity : class, IEntity<TKey>
        where TUserEntity : class, IUserEntity<TKey>
        where TKey : notnull
    {
        Task<bool> ValidateUserAccessAsync(string userId, TKey entityId);
    }
}
