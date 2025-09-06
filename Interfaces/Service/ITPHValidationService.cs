namespace Calibr8Fit.Api.Interfaces.Service
{
    public interface ITPHValidationService<TKey>
        where TKey : notnull
    {
        Task<bool> ValidateUserAccessAsync(string userId, TKey entityId);
    }
}
