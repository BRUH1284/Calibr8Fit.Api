using Calibr8Fit.Api.Data;
using Calibr8Fit.Api.Interfaces.Repository;
using Calibr8Fit.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Calibr8Fit.Api.Repository
{
    public class UserActivityRepository(ApplicationDbContext context) : RepositoryBase(context), IUserActivityRepository
    {
        public async Task<string> GenerateUserDataChecksumAsync(string userId)
        {
            // Get all user activities for the specified user
            var userActivities = await _context.UserActivities
                .Where(ua => ua.UserId == userId)
                .Select(ua => new
                {
                    ua.MajorHeading,
                    ua.MetValue,
                    ua.Description,
                    ua.ModifiedAt
                })
                .ToListAsync();

            // Generate checksum for the user activities
            return ((IDataChecksumProvider)this).GenerateChecksum(userActivities);
        }
        public async Task<UserActivity?> AddAsync(UserActivity userActivity)
        {
            // Check if user activity already exists
            if (await _context.BaseActivities.FindAsync(userActivity.Id) is not null)
                return null;

            // Add new user activity to DB
            await _context.AddAsync(userActivity);
            await _context.SaveChangesAsync();
            return userActivity;
        }
        public async Task<List<UserActivity>> AddRangeAsync(IEnumerable<UserActivity> userActivities)
        {
            // Convert the IEnumerable to a List for EF Core
            var activitiesList = userActivities.ToList();

            // Check if any user activity already exists
            var existingActivities = await _context.UserActivities
                .Where(ua => activitiesList.Select(a => a.Id).Contains(ua.Id))
                .ToListAsync();

            // Filter out existing activities
            var newActivities = activitiesList
                .Where(a => !existingActivities.Any(ea => ea.Id == a.Id))
                .ToList();

            if (newActivities.Count == 0)
                return [];

            // Add multiple user activities to DB
            await _context.AddRangeAsync(activitiesList);
            await _context.SaveChangesAsync();
            return activitiesList;
        }
        public async Task<UserActivity?> GetByIdAsync(Guid id)
        {
            // Get user activity by id
            return await _context.UserActivities.FirstAsync(ua => ua.Id == id);
        }
        public async Task<UserActivity?> GetByUserIdAndIdAsync(string userId, Guid id)
        {
            // Get user activity by userId and id
            return await _context.UserActivities
                .FirstOrDefaultAsync(ua => ua.UserId == userId && ua.Id == id);
        }
        public async Task<List<UserActivity>> GetAllByUserIdAsync(string userId)
        {
            // Get all user activities for a specific user
            return await _context.UserActivities
                .Where(ua => ua.UserId == userId)
                .ToListAsync();
        }
        public async Task<UserActivity?> UpdateByUserIdAsync(string userId, UserActivity updatedActivity)
        {
            // Get existing activity by userId and id
            var existingUserActivity = await _context.UserActivities
                .FirstOrDefaultAsync(ua => ua.UserId == userId && ua.Id == updatedActivity.Id);
            if (existingUserActivity is null) return null;

            // Update properties
            UpdateProperties(existingUserActivity, updatedActivity);

            // Save changes in DB
            await _context.SaveChangesAsync();
            return existingUserActivity;
        }
        public async Task<UserActivity?> UpdateAsync(UserActivity updatedActivity)
        {
            // Get existing activity by id
            var existingUserActivity = await _context.UserActivities.FirstAsync(ua => ua.Id == updatedActivity.Id);
            if (existingUserActivity is null) return null;

            // Update properties
            UpdateProperties(existingUserActivity, updatedActivity);

            // Save changes in DB
            await _context.SaveChangesAsync();
            return existingUserActivity;
        }
        public async Task<List<UserActivity>> UpdateRangeByUserIdAsync(string userId, IEnumerable<UserActivity> updatedActivities)
        {
            var existingActivities = await _context.UserActivities
                .Where(ua => ua.UserId == userId && updatedActivities.Select(r => r.Id).Contains(ua.Id))
                .ToListAsync();

            foreach (var existingUserActivity in existingActivities)
            {
                // Find the corresponding updatedActivity and update properties
                var updatedActivity = updatedActivities.FirstOrDefault(r => r.Id == existingUserActivity.Id);
                UpdateProperties(existingUserActivity, updatedActivity!);
            }

            // Save changes in DB
            await _context.SaveChangesAsync();
            return existingActivities;
        }
        private static UserActivity UpdateProperties(UserActivity existingUserActivity, UserActivity updateActivity)
        {
            // Update properties
            existingUserActivity.MajorHeading = updateActivity.MajorHeading;
            existingUserActivity.MetValue = updateActivity.MetValue;
            existingUserActivity.Description = updateActivity.Description;
            existingUserActivity.ModifiedAt = updateActivity.ModifiedAt;
            existingUserActivity.Deleted = updateActivity.Deleted;

            // Update synced time
            existingUserActivity.SyncedAt = DateTime.UtcNow;

            return existingUserActivity;
        }
        public async Task<UserActivity?> DeleteByUserIdAndIdAsync(string userId, Guid id, DateTime deletedAt)
        {
            // Get existing activity by userId and id
            var existingUserActivity = await _context.UserActivities
                .FirstOrDefaultAsync(ua => ua.UserId == userId && ua.Id == id);
            if (existingUserActivity is null) return null;

            // Remove activity from DB
            await DeleteAsync(existingUserActivity, deletedAt);
            return existingUserActivity;
        }
        public async Task<UserActivity?> DeleteAsync(Guid id, DateTime deletedAt)
        {
            // Get existing activity by code
            var existingUserActivity = await _context.UserActivities.FirstAsync(ua => ua.Id == id);
            if (existingUserActivity is null) return null;

            // Remove activity from DB
            await DeleteAsync(existingUserActivity, deletedAt);
            return existingUserActivity;
        }
        public async Task<List<UserActivity>> DeleteRangeByUserIdAndIdAsync(string userId, IEnumerable<(Guid id, DateTime deletedAt)> ids)
        {
            // Get existing activities by userId and ids
            var existingActivities = await _context.UserActivities
                .Where(ua => ua.UserId == userId && ids.Select(x => x.id).Contains(ua.Id))
                .ToListAsync();

            if (existingActivities.Count == 0)
                return [];

            // Mark each existing activity as deleted
            foreach (var existingUserActivity in existingActivities)
            {
                // Find the corresponding ModifiedAt value
                existingUserActivity.ModifiedAt = ids.First(x => x.id == existingUserActivity.Id).deletedAt;
                // Mark as deleted
                existingUserActivity.Deleted = true;
                // Update synced time
                existingUserActivity.SyncedAt = DateTime.UtcNow;
            }
            ;

            await _context.SaveChangesAsync();
            return existingActivities;
        }
        private async Task DeleteAsync(UserActivity userActivity, DateTime deletedAt)
        {
            // Set ModifiedAt property
            userActivity.ModifiedAt = deletedAt;
            // Mark as deleted
            userActivity.Deleted = true;
            // Update synced time
            userActivity.SyncedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }
}