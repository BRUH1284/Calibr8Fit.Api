using Calibr8Fit.Api.Data;
using Calibr8Fit.Api.Interfaces.Repository;
using Calibr8Fit.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Calibr8Fit.Api.Repository
{
    public class RefreshTokenRepository(
        ApplicationDbContext context
        ) : UserRepositoryBase<RefreshToken>(context), IRefreshTokenRepository
    {
        // Get by UserId and DeviceId
        public async Task<RefreshToken?> GetByUserIdAndDeviceIdAsync(string userId, string deviceId)
        {
            // Find the refresh token by UserId and DeviceId
            return await _dbSet
                .Where(rt => rt.UserId == userId && rt.DeviceId == deviceId)
                .FirstOrDefaultAsync();
        }
        // Delete by ID
        public async Task<RefreshToken?> DeleteByUserIdAndDeviceIdAsync(string userId, string deviceId)
        {
            // Find the refresh token by UserId and DeviceId
            var refreshToken = await GetByUserIdAndDeviceIdAsync(userId, deviceId);

            // Remove the refresh token if it exists
            return await RemoveEntityAsync(refreshToken);
        }

        protected override async Task<RefreshToken?> GetEntityAsync(RefreshToken entity)
        {
            // Get the refresh token by UserId and DeviceId
            return await GetByUserIdAndDeviceIdAsync(entity.UserId, entity.DeviceId);
        }
    }
}