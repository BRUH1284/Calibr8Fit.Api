namespace Calibr8Fit.Api.Interfaces.Service
{
    public interface IActivityValidationService
    {
        Task<bool> ValidateActivityLinkAsync(string userId, Guid activityId);
    }
}
