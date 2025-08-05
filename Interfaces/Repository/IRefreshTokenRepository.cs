using Calibr8Fit.Api.Models;

namespace Calibr8Fit.Api.Interfaces.Repository
{
    public interface IRefreshTokenRepository : IUserRepositoryBase<RefreshToken>
    {
        Task<RefreshToken?> GetByUserIdAndDeviceIdAsync(string userId, string deviceId);
        Task<RefreshToken?> DeleteByUserIdAndDeviceIdAsync(string userId, string deviceId);
    }
}