using Calibr8Fit.Api.Data;
using Calibr8Fit.Api.DataTransferObjects.Activity;
using Calibr8Fit.Api.Enums;
using Calibr8Fit.Api.Interfaces;
using Calibr8Fit.Api.Interfaces.Repository;
using Calibr8Fit.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Calibr8Fit.Api.Repository
{
    public class ActivityRepository(
        IDataVersionRepository dataVersionRepository,
        ApplicationDbContext context) : RepositoryBase(context), IActivityRepository
    {
        private static readonly DataResource DataResource = DataResource.Activities;
        IDataVersionRepository IDataVersionProvider.DataVersionRepository => dataVersionRepository;
        DataResource IDataVersionProvider.DataResource => DataResource;
        public async Task<Activity?> AddAsync(Activity activity)
        {
            // Check if activity already exists
            if (await _context.Activities.FindAsync(activity.Code) is not null)
                return null;

            // Add new activity to DB
            await _context.Activities.AddAsync(activity);
            await SaveChangesAsync();
            return activity;
        }
        public async Task<Activity?> GetByCodeAsync(int code)
        {
            // Get activity by code
            return await _context.Activities.FindAsync(code);
        }
        public async Task<List<Activity>> GetAllAsync()
        {
            // Get all activities
            return await _context.Activities.ToListAsync();
        }
        public async Task<Activity?> UpdateAsync(int code, UpdateActivityRequestDto updateRequest)
        {
            // Get existing activity by code
            var existingActivity = await _context.Activities.FindAsync(code);
            if (existingActivity is null) return null;

            // Update properties
            existingActivity.MajorHeading = updateRequest.MajorHeading;
            existingActivity.MetValue = updateRequest.MetValue;
            existingActivity.Description = updateRequest.Description;

            // Save changes in DB
            await SaveChangesAsync();
            return existingActivity;
        }
        public async Task<Activity?> DeleteAsync(int code)
        {
            // Get existing activity by code
            var existingActivity = await _context.Activities.FindAsync(code);
            if (existingActivity is null) return null;

            // Remove activity from DB
            _context.Activities.Remove(existingActivity);
            await SaveChangesAsync();
            return existingActivity;
        }
        private async Task SaveChangesAsync()
        {
            // Save changes in DB
            await _context.SaveChangesAsync();
            // Update data version
            await dataVersionRepository.AddOrUpdateAsync(DataResource);
        }
    }
}