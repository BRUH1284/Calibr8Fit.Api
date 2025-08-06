using Calibr8Fit.Api.Data;
using Calibr8Fit.Api.Interfaces.Repository;
using Calibr8Fit.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Calibr8Fit.Api.Repository
{
    public class UserActivityRepository(
        ApplicationDbContext context
    ) : UserRepositoryBase<UserActivity, ActivityBase, Guid>(context), IUserActivityRepository
    {
        public Task<DateTime> GetLastSyncedAtAsync(string userId)
        {
            return _dbSet
                .Where(ua => ua.UserId == userId)
                .Select(ua => ua.SyncedAt)
                .DefaultIfEmpty(DateTime.MinValue)
                .MaxAsync();
        }
        public async Task<List<UserActivity>> GetAllFromDateByUserIdAsync(DateTime fromDate, string userId)
        {
            // Get all user activities for a specific user that have been synced after a certain date
            return await _dbSet
                .Where(ua => ua.UserId == userId && ua.SyncedAt > fromDate)
                .ToListAsync();
        }
    }
}