using Calibr8Fit.Api.Data;
using Calibr8Fit.Api.DataTransferObjects.Activity;
using Calibr8Fit.Api.Interfaces;
using Calibr8Fit.Api.Interfaces.Repository;
using Calibr8Fit.Api.Mappers;
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
                    ua.UpdatedAt
                })
                .ToListAsync();

            // Generate checksum for the user activities
            return ((IDataChecksumProvider)this).GenerateChecksum(userActivities);
        }
        public async Task<UserActivity> AddAsync(UserActivity userActivity)
        {
            // Add new user activity to DB
            await _context.UserActivities.AddAsync(userActivity);
            await _context.SaveChangesAsync();
            return userActivity;
        }
        public async Task<List<UserActivity>> AddRangeAsync(IEnumerable<UserActivity> userActivities)
        {
            // Convert the IEnumerable to a List for EF Core
            var activitiesList = userActivities.ToList();
            // Add multiple user activities to DB
            await _context.UserActivities.AddRangeAsync(activitiesList);
            await _context.SaveChangesAsync();
            return activitiesList;
        }
        public async Task<UserActivity?> GetByIdAsync(Guid id)
        {
            // Get user activity by id
            return await _context.UserActivities.FindAsync(id);
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
        public async Task<UserActivity?> UpdateByUserIdAsync(string userId, UpdateUserActivityRequestDto updateRequest)
        {
            // Get existing activity by userId and id
            var existingUserActivity = await _context.UserActivities
                .FirstOrDefaultAsync(ua => ua.UserId == userId && ua.Id == updateRequest.Id);
            if (existingUserActivity is null) return null;

            // Update properties
            UpdateProperties(existingUserActivity, updateRequest);

            // Save changes in DB
            await _context.SaveChangesAsync();
            return existingUserActivity;
        }
        public async Task<UserActivity?> UpdateAsync(UpdateUserActivityRequestDto requestDto)
        {
            // Get existing activity by id
            var existingUserActivity = await _context.UserActivities.FindAsync(requestDto.Id);
            if (existingUserActivity is null) return null;

            // Update properties
            UpdateProperties(existingUserActivity, requestDto);

            // Save changes in DB
            await _context.SaveChangesAsync();
            return existingUserActivity;
        }
        public async Task<List<UserActivity>> UpdateRangeByUserIdAsync(string userId, IEnumerable<UpdateUserActivityRequestDto> requestDtos)
        {
            var existingActivities = await _context.UserActivities
                .Where(ua => ua.UserId == userId && requestDtos.Select(r => r.Id).Contains(ua.Id))
                .ToListAsync();

            foreach (var existingUserActivity in existingActivities)
            {
                // Find the corresponding requestDto and update properties
                var requestDto = requestDtos.FirstOrDefault(r => r.Id == existingUserActivity.Id);
                UpdateProperties(existingUserActivity, requestDto!);
            }

            // Save changes in DB
            await _context.SaveChangesAsync();
            return existingActivities;
        }
        private static UserActivity UpdateProperties(UserActivity existingUserActivity, UpdateUserActivityRequestDto requestDto)
        {
            existingUserActivity.MajorHeading = requestDto.MajorHeading;
            existingUserActivity.MetValue = requestDto.MetValue;
            existingUserActivity.Description = requestDto.Description;
            existingUserActivity.UpdatedAt = requestDto.UpdatedAt;

            return existingUserActivity;
        }
        public async Task<UserActivity?> DeleteByUserIdAndIdAsync(string userId, Guid id)
        {
            // Get existing activity by userId and id
            var existingUserActivity = await _context.UserActivities
                .FirstOrDefaultAsync(ua => ua.UserId == userId && ua.Id == id);
            if (existingUserActivity is null) return null;

            // Remove activity from DB
            await RemoveAsync(existingUserActivity);
            return existingUserActivity;
        }
        public async Task<UserActivity?> DeleteAsync(Guid id)
        {
            // Get existing activity by code
            var existingUserActivity = await _context.UserActivities.FindAsync(id);
            if (existingUserActivity is null) return null;

            // Remove activity from DB
            await RemoveAsync(existingUserActivity);
            return existingUserActivity;
        }
        public async Task<List<UserActivity>> DeleteRangeByUserIdAndIdAsync(string userId, IEnumerable<Guid> ids)
        {
            // Get existing activities by userId and ids
            var existingActivities = await _context.UserActivities
                .Where(ua => ua.UserId == userId && ids.Contains(ua.Id))
                .ToListAsync();

            // Remove activities from DB
            _context.UserActivities.RemoveRange(existingActivities);

            await _context.SaveChangesAsync();
            return existingActivities;
        }
        private async Task RemoveAsync(UserActivity userActivity)
        {
            // Remove user activity from DB
            _context.UserActivities.Remove(userActivity);
            await _context.SaveChangesAsync();
        }
    }
}