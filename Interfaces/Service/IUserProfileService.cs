using Calibr8Fit.Api.DataTransferObjects.User;
using Calibr8Fit.Api.Models;
using Calibr8Fit.Api.Services.Results;
using Microsoft.AspNetCore.JsonPatch;

namespace Calibr8Fit.Api.Interfaces.Service;

public interface IUserProfileService
{
    Task<UserProfileSettingsDto> SyncUserProfileSettingsAsync(User user, JsonPatchDocument<UserProfileSettingsPatchDto> patch);
    Task<Result> UploadProfilePictureAsync(User user, IFormFile file);
    Task<Result> DeleteProfilePictureAsync(User user, string fileName);
    Task<Result> DeleteMyProfilePictureAsync(User user);
    Task<Result> UpdateProfilePictureFileName(User user, string profilePictureFileName);
}
