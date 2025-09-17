using Calibr8Fit.Api.Interfaces.Service;
using Calibr8Fit.Api.Models;

namespace Calibr8Fit.Api.Extensions
{
    public static class UserExtensions
    {
        public static string? GetProfilePictureUrl(this User user, HttpRequest request, IPathService pathService)
        {
            if (user?.UserName == null || user?.Profile?.ProfilePictureFileName == null)
                return null;

            return pathService.GetProfilePictureUrl(
                request,
                user.UserName,
                user.Profile.ProfilePictureFileName
                );
        }
    }
}