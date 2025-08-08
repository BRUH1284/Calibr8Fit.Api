using Calibr8Fit.Api.Interfaces.Repository;
using Calibr8Fit.Api.Interfaces.Service;

namespace Calibr8Fit.Api.Services
{
    public class ActivityValidationService(
        IActivityRepository activityRepository,
        IUserActivityRepository userActivityRepository
    ) : IActivityValidationService
    {
        private readonly IActivityRepository _activityRepository = activityRepository;
        private readonly IUserActivityRepository _userActivityRepository = userActivityRepository;
        public async Task<bool> ValidateActivityLinkAsync(string userId, Guid activityId)
        {
            // Check if activity exists
            if (await _activityRepository.KeyExistsAsync(activityId))
                return true;

            // If activity does not exist, check user activity
            if (await _userActivityRepository.UserKeyExistsAsync(userId, activityId))
                return true;

            return false;
        }
    }
}