using Calibr8Fit.Api.Data;
using Calibr8Fit.Api.Interfaces.Repository;
using Calibr8Fit.Api.Models;

namespace Calibr8Fit.Api.Repository
{
    public class RefreshTokenRepository(
        ApplicationDbContext context
        ) : UserRepositoryBase<RefreshToken, string[]>(context), IRefreshTokenRepository
    {
        public override async Task<RefreshToken?> GetAsync(string[] key)
        {
            // Validate key length
            if (key.Length != 2)
                throw new ArgumentException("Key must contain exactly two elements: UserId and DeviceId.");

            // Get the refresh token by UserId and DeviceId
            return await _dbSet.FindAsync(key[0], key[1]);
        }
        protected override async Task<RefreshToken?> GetEntityAsync(RefreshToken entity)
        {
            // Get the refresh token by UserId and DeviceId
            return await _dbSet.FindAsync(entity.UserId, entity.DeviceId);
        }
    }
}