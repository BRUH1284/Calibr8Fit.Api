using Calibr8Fit.Api.Interfaces.Service;
using Calibr8Fit.Api.Options;
using Microsoft.Extensions.Options;

namespace Calibr8Fit.Api.Services
{
    public class PathService(IOptions<StorageOptions> options) : IPathService
    {
        private readonly StorageOptions _options = options.Value;

        private static string EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }
        public string GetUserUploadsDirectoryPath(string username) =>
            EnsureDirectoryExists(Path.Combine(
                _options.RootPath,
                _options.UploadsPath,
                username));
        public string GetProfilePictureDirectoryPath(string username) =>
            EnsureDirectoryExists(Path.Combine(
                    GetUserUploadsDirectoryPath(username),
                    _options.ProfilePicturesSubfolder
            ));
        public string? GetProfilePicturePath(string username, string fileName)
        {
            var path = Path.Combine(
                GetProfilePictureDirectoryPath(username),
                fileName
            );

            // Return path if it exists
            return File.Exists(path) ? path : null;
        }
        public string? GetProfilePictureUrl(HttpRequest httpRequest, string username, string fileName)
        {
            var path = GetProfilePicturePath(username, fileName);
            // If path exists return public url
            return path == null ? null : BuildPublicUrl(httpRequest, path);
        }
        public string GetPostImagesDirectoryPath(string username, int postId) =>
            EnsureDirectoryExists(Path.Combine(
                GetUserUploadsDirectoryPath(username)!,
                _options.PostSubfolder,
                postId.ToString(),
                _options.PostImagesSubfolder
            ));
        public string? GetPostImagePath(string username, int postId, string fileName)
        {
            var path = Path.Combine(
                GetPostImagesDirectoryPath(username, postId),
                fileName
            );

            // Return path if it exists
            return File.Exists(path) ? path : null;
        }
        public string? GetPostImageUrl(HttpRequest httpRequest, string username, int postId, string fileName)
        {
            var path = GetPostImagePath(username, postId, fileName);

            // If path exists return public url
            return path == null ? null : BuildPublicUrl(httpRequest, path);
        }
        public string RemoveRoot(string path) =>
            path.StartsWith(_options.RootPath) ?
                path.Substring(_options.RootPath.Length + 1) :
                path;
        public string AddRoot(string path) =>
            path.StartsWith(_options.RootPath) ?
                path :
                Path.Combine(_options.RootPath, path);
        public string BuildPublicUrl(HttpRequest request, string relativePath)
        {
            // Normalize path and remove leading "wwwroot/" if present
            var cleanedPath = relativePath.Replace("\\", "/");
            if (cleanedPath.StartsWith(_options.RootPath))
                cleanedPath = cleanedPath.Substring($"{_options.RootPath}/".Length);
            // Return url
            return $"{request.Scheme}://{request.Host}/{cleanedPath}";
        }
        public string? PublicUrlToRelativePath(string url)
        {
            // Generate path
            var path = Path.Combine(_options.RootPath, Path.Combine(new Uri(url).LocalPath.Split('/')));
            // Return path if it exists
            return File.Exists(path) ? path : null;
        }
    }
}