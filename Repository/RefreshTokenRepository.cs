using Calibr8Fit.Api.Data;
using Calibr8Fit.Api.Interfaces.Repository;
using Calibr8Fit.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Calibr8Fit.Api.Repository
{
    public class RefreshTokenRepository : RepositoryBase, IRefreshTokenRepository
    {
        public RefreshTokenRepository(ApplicationDbContext context) : base(context) { }
        // Get by UserId
        public async Task<List<RefreshToken>> GetByUserIdAsync(string userId)
        {
            return await _context.RefreshTokens.Where(rt => rt.UserId == userId).ToListAsync();
        }
        // Get by UserId and DeviceId
        public async Task<RefreshToken?> GetByUserIdAndDeviceIdAsync(string userId, string deviceId)
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.UserId == userId && rt.DeviceId == deviceId);
        }
        // Delete by ID
        public async Task<RefreshToken?> DeleteAsync(string userId, string deviceId)
        {
            var refreshTokenModel = await _context.RefreshTokens.FindAsync(userId, deviceId);

            if (refreshTokenModel == null)
            {
                return null;
            }

            _context.RefreshTokens.Remove(refreshTokenModel);
            await _context.SaveChangesAsync();
            return refreshTokenModel;
        }
        // Delete all tokens for a user
        public async Task<List<RefreshToken>?> DeleteAllAsync(string userId)
        {
            var refreshTokens = await _context.RefreshTokens.Where(rt => rt.UserId == userId).ToListAsync();

            if (refreshTokens.Count == 0)
                return null;

            _context.RefreshTokens.RemoveRange(refreshTokens);
            await _context.SaveChangesAsync();
            return refreshTokens;
        }
        // Update or create token
        public async Task<RefreshToken> UpdateOrCreateAsync(RefreshToken refreshToken)
        {
            // Check if the token already exists (based on UserId and DeviceId)
            var existingToken = await GetByUserIdAndDeviceIdAsync(refreshToken.UserId, refreshToken.DeviceId);

            if (existingToken != null)
            {
                // Token exists, update it
                existingToken.TokenHash = refreshToken.TokenHash;
                existingToken.ExpiresOn = refreshToken.ExpiresOn;
                _context.RefreshTokens.Update(existingToken);
            }
            else
            {
                // Token does not exist, create a new one
                await _context.RefreshTokens.AddAsync(refreshToken);
            }

            // Save changes to the database
            await _context.SaveChangesAsync();
            return refreshToken;
        }
    }
}