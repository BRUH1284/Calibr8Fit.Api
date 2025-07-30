using Calibr8Fit.Api.Data;
using Calibr8Fit.Api.DataTransferObjects.User;
using Calibr8Fit.Api.Interfaces.Repository;
using Calibr8Fit.Api.Models;

namespace Calibr8Fit.Api.Repository
{
    public class UserProfileRepository(ApplicationDbContext context) : RepositoryBase(context), IUserProfileRepository
    {
        public async Task<UserProfile> AddAsync(UserProfile userProfile)
        {
            // Add new user profile to DB
            await _context.UserProfiles.AddAsync(userProfile);
            await _context.SaveChangesAsync();
            return userProfile;
        }
        public async Task<UserProfile?> UpdateAsync(string id, UserProfileSettingsDto request)
        {
            // Get user profile by id
            var existingProfile = await _context.UserProfiles.FindAsync(id);
            // Profile dont exist
            if (existingProfile is null) return null;

            Console.WriteLine($"Updating profile for user ID: {id}");
            Console.WriteLine(request.UserName);

            existingProfile.FirstName = request.FirstName ?? existingProfile.FirstName;
            existingProfile.LastName = request.LastName ?? existingProfile.LastName;
            existingProfile.DateOfBirth = request.DateOfBirth ?? existingProfile.DateOfBirth;
            existingProfile.Gender = request.Gender ?? existingProfile.Gender;
            existingProfile.Weight = request.Weight ?? existingProfile.Weight;
            existingProfile.TargetWeight = request.TargetWeight ?? existingProfile.TargetWeight;
            existingProfile.Height = request.Height ?? existingProfile.Height;
            existingProfile.ActivityLevel = request.ActivityLevel ?? existingProfile.ActivityLevel;
            existingProfile.Climate = request.Climate ?? existingProfile.Climate;

            // Save changes in DB
            await _context.SaveChangesAsync();
            return existingProfile;
        }
    }
}