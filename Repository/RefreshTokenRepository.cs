using Calibr8Fit.Api.Data;
using Calibr8Fit.Api.Interfaces.Repository;
using Calibr8Fit.Api.Models;
using Calibr8Fit.Api.Repository.Base;

namespace Calibr8Fit.Api.Repository
{
    public class RefreshTokenRepository(
        ApplicationDbContext context
        ) : UserRepositoryBase<RefreshToken, string[]>(context)
    {
        public override ValueTask<RefreshToken?> GetAsync(string[] key)
        {
            // Validate key length
            if (key.Length != 2)
                throw new ArgumentException("Key must contain exactly two elements: UserId and DeviceId.");

            // Get the refresh token by UserId and DeviceId
            return _dbSet.FindAsync(key[0], key[1]);
        }
        protected override ValueTask<RefreshToken?> GetEntityAsync(RefreshToken entity)
        {
            // Get the refresh token by UserId and DeviceId
            return _dbSet.FindAsync(entity.UserId, entity.DeviceId);
        }
        public override async Task<bool> KeyExistsInHierarchyAsync(string[] key)
        {
            // Check if the hierarchy key exists in the database
            return await GetAsync(key) is not null;
        }
    }
}