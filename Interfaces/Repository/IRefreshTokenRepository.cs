using Calibr8Fit.Api.Models;

namespace Calibr8Fit.Api.Interfaces.Repository
{
    public interface IRefreshTokenRepository
    {
        Task<List<RefreshToken>> GetByUserIdAsync(string userId);
        Task<RefreshToken?> GetByUserIdAndDeviceIdAsync(string userId, string deviceId);
        Task<RefreshToken?> DeleteAsync(string userId, string deviceId);
        Task<List<RefreshToken>?> DeleteAllAsync(string userId);
        Task<RefreshToken> UpdateOrCreateAsync(RefreshToken refreshToken);
    }
}