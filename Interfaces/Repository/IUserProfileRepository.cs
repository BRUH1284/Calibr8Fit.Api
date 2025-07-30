using Calibr8Fit.Api.DataTransferObjects.User;
using Calibr8Fit.Api.Models;

namespace Calibr8Fit.Api.Interfaces.Repository
{
    public interface IUserProfileRepository
    {
        Task<UserProfile> AddAsync(UserProfile userProfile);
        Task<UserProfile?> UpdateAsync(string id, UserProfileSettingsDto request);
    }
}