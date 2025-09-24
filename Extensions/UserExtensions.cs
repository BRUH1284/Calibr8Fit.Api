using Calibr8Fit.Api.Interfaces.Service;
using Calibr8Fit.Api.Models;

namespace Calibr8Fit.Api.Extensions
{
    public static class UserExtensions
    {
        public static string? GetProfilePictureUrl(this User user, IPathService pathService)
        {
            if (user?.UserName == null || user?.Profile?.ProfilePictureFileName == null)
                return null;

            return pathService.GetProfilePictureUrl(
                user.UserName,
                user.Profile.ProfilePictureFileName
                );
        }
    }
}